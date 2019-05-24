
'*****************************************************************************
' This file is part of jHDF. A pure Java library for accessing HDF5 files.
' 
' http://jhdf.io
' 
' Copyright 2019 James Mudd
' 
' MIT License see 'LICENSE' file
' *****************************************************************************

Imports System.IO
Imports Microsoft.VisualBasic.Data.IO.HDF5.struct
Imports Microsoft.VisualBasic.Serialization.JSON

Namespace HDF5.dataset

    ''' <summary>
    ''' Chunked: The array domain is regularly decomposed into chunks, and each chunk is allocated and 
    ''' stored separately. This layout supports arbitrary element traversals, compression, encryption, 
    ''' and checksums (these features are described in other messages). The message stores the size of 
    ''' a chunk instead of the size of the entire array; the storage size of the entire array can be 
    ''' calculated by traversing the chunk index that stores the chunk addresses.
    ''' 
    ''' This represents chunked datasets using a b-tree for indexing raw data chunks.
    ''' It supports filters for use when reading the dataset for example to
    ''' decompress.
    ''' 
    ''' @author James Mudd
    ''' </summary>
    Public Class ChunkedDatasetV3 : Inherits Hdf5Dataset

        ''' <summary>
        ''' A chunk has a fixed dimensionality. This field specifies the number of dimension size 
        ''' fields later in the message.
        ''' </summary>
        ''' <returns></returns>
        Public Property dimensionality As Integer
        ''' <summary>
        ''' This is the address of the v1 B-tree that is used to look up the addresses of the chunks 
        ''' that actually store portions of the array data. The address may have the 
        ''' ��undefined address�� value, to indicate that storage has not yet been allocated for this 
        ''' array.
        ''' </summary>
        ''' <returns></returns>
        Public Property BtreeAddress As Long

        ''' <summary>
        ''' These values define the dimension size of a single chunk, in units of array elements 
        ''' (not bytes). The first dimension stored in the list of dimensions is the slowest changing 
        ''' dimension and the last dimension stored is the fastest changing dimension.
        ''' </summary>
        ''' <returns></returns>
        Public Property dimensionSize As Integer()

        ''' <summary>
        ''' ###### Dataset Element Size
        ''' 
        ''' The size of a dataset element, in bytes.
        ''' </summary>
        ''' <returns></returns>
        Public Property byteSize As Integer

        ReadOnly decodedChunkLookup As New Dictionary(Of ChunkOffsetKey, Byte())()

        Public Overridable ReadOnly Property size() As Long
            Get
                Return dataSpace.totalLength
            End Get
        End Property

        Public Overridable ReadOnly Property diskSize() As Long
            Get
                Return size * dataType.size
            End Get
        End Property

        Public Overridable ReadOnly Property dimensions() As Integer()
            Get
                Return dataSpace.dimensionLength
            End Get
        End Property

        Public Overridable ReadOnly Property maxSize() As Integer()
            Get
                If Not dataSpace.maxDimensionLength.IsNullOrEmpty Then
                    Return dataSpace.maxDimensionLength
                Else
                    Return dimensions
                End If
            End Get
        End Property

        ''' <summary>
        ''' ��һ�����͵������±�ת��Ϊ��ά���������������Ϣ
        ''' </summary>
        ''' <param name="index"></param>
        ''' <param name="dimensions"></param>
        ''' <returns></returns>
        Private Function linearIndexToDimensionIndex(index As Integer, dimensions As Integer()) As Integer()
            Dim dimIndex As Integer() = New Integer(dimensions.Length - 1) {}

            For i As Integer = dimIndex.Length - 1 To 0 Step -1
                dimIndex(i) = index Mod dimensions(i)
                index = index \ dimensions(i)
            Next
            Return dimIndex
        End Function

        ''' <summary>
        ''' ��һ����ά���������������Ϣת��Ϊһ��һά��������������±�
        ''' </summary>
        ''' <param name="index"></param>
        ''' <param name="dimensions"></param>
        ''' <returns></returns>
        Private Function dimensionIndexToLinearIndex(index As Integer(), dimensions As Integer()) As Integer
            Dim linear As Integer = 0

            If index.All(Function(i) i = 0) Then
                ' ���е�Ԫ�ض����㣬����϶�����������֮�еĵ�һ��Ԫ��
                Return 0
            End If

            For i As Integer = 0 To dimensions.Length - 1
                Dim temp As Integer = index(i)

                For j As Integer = i + 1 To dimensions.Length - 1
                    temp *= dimensions(j)
                Next

                linear += temp
            Next

            Return linear
        End Function

        Private Function getChunkOffset(dimensionedIndex As Integer()) As Long()
            Dim chunkOffset As Long() = New Long(dimensionedIndex.Length - 1) {}

            For i As Integer = 0 To chunkOffset.Length - 1
                Dim temp As Long = dataLayout.chunkSize(i)
                chunkOffset(i) = (dimensionedIndex(i) \ temp) * temp
            Next

            Return chunkOffset
        End Function

        Protected Overrides Function getBuffer(sb As Superblock) As MemoryStream
            Return getBuffer(sb, New ChunkLookup(sb, Me))
        End Function

        Private Overloads Function getBuffer(sb As Superblock, chunkLookup As ChunkLookup) As MemoryStream
            ' Need to load the full buffer into memory so create the array
            Dim dataArray As Byte() = New Byte(diskSize - 1) {}
            Dim elementSize As Integer = dataType.size

            ' size ��Ԫ�ص��������������ѭ��֮�У��ֱ�������꣬��ÿһ��Ԫ�ص������ֽڴӶ�Ӧ��chunk֮�и��Ƶ�dataArray֮��
            For i As Integer = 0 To size - 1

                ' ���������ȸ���Ԫ�ص��ֽ�ռ����������Ԫ��������chunk�ı��
                Dim dimensionedIndex As Integer() = linearIndexToDimensionIndex(i, dimensions)
                Dim chunkOffset As Long() = getChunkOffset(dimensionedIndex)

                ' Now figure out which element inside the chunk
                Dim insideChunk As Integer() = New Integer(chunkOffset.Length - 1) {}

                For j As Integer = 0 To chunkOffset.Length - 1
                    insideChunk(j) = CInt(dimensionedIndex(j) - chunkOffset(j))
                Next

                ' Ȼ������Ĵ�����������������chunk��Ų��ҳ���Ӧ��chunk
                Dim insideChunkLinearOffset As Integer = dimensionIndexToLinearIndex(insideChunk, dataLayout.chunkSize)
                Dim key As New ChunkOffsetKey(chunkOffset)
                Dim chunkData As Byte() = getDecodedChunk(chunkLookup, key)

                ' Copy that data into the overall buffer
                ' Ȼ��������ҳ�����chunk֮�и��ƶ�Ӧ��Ԫ�ص�dataarray֮��
                Dim sourcePos = insideChunkLinearOffset * elementSize
                Dim dataOffset = i * elementSize

                Array.Copy(chunkData, sourcePos, dataArray, dataOffset, elementSize)
            Next

            Return New MemoryStream(dataArray)
        End Function

        Private Function getDecodedChunk(chunkLookup As ChunkLookup, chunkKey As ChunkOffsetKey) As Byte()
            Return decodedChunkLookup.ComputeIfAbsent(
                chunkKey, Function(key)
                              Dim entry = chunkLookup(chunkKey)
                              Return decodeChunk(entry, chunkLookup.sb)
                          End Function)
        End Function

        Private Function decodeChunk(chunk As DataChunk, sb As Superblock) As Byte()
            ' Get the encoded (i.e. compressed buffer)
            ' Get the encoded data from buffer
            Dim encodedBytes As Byte() = getDataBuffer(sb, chunk)

            If pipeline Is Nothing Then
                ' No filters
                Return encodedBytes
            Else
                ' Decode using the pipeline applying the filters
                Dim decodedBytes As Byte() = pipeline.decode(encodedBytes)
                Return decodedBytes
            End If
        End Function

        Private Function getDataBuffer(sb As Superblock, chunk As DataChunk) As Byte()
            Return sb.FileReader(chunk.filePosition).readBytes(chunk.size)
        End Function
    End Class

    Public Class ChunkLookup

        ReadOnly lookup As Dictionary(Of String, DataChunk)

        Public ReadOnly Property sb As Superblock

        Public ReadOnly Property chunkValues As DataChunk()
            Get
                Return lookup.Values.ToArray
            End Get
        End Property

        Default Public ReadOnly Property GetChunk(offsetKey As ChunkOffsetKey) As DataChunk
            Get
                Return lookup(offsetKey.key)
            End Get
        End Property

        Sub New(sb As Superblock, dataset As ChunkedDatasetV3)
            Dim bTree As New DataBTree(dataset.dataLayout)
            Dim chunkLookupMap As New Dictionary(Of String, DataChunk)()

            For Each chunk As DataChunk In bTree.EnumerateChunks(sb)
                chunkLookupMap(New ChunkOffsetKey(chunk.offsets).key) = chunk
            Next

            Me.lookup = chunkLookupMap
            Me.sb = sb
        End Sub

    End Class

    ''' <summary>
    ''' Custom key object for indexing chunks. It is optimised for fast hashcode and
    ''' equals when looking up chunks.
    ''' </summary>
    Public Class ChunkOffsetKey

        Friend ReadOnly hashcode As Integer
        Friend ReadOnly chunkOffset As Long()

        ''' <summary>
        ''' �ֵ���ҵ�������
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' ��Ϊ��java֮�к���VB.NET֮�е��ֵ���ҵ�ԭ��һ��������������ʹ������ַ�����Ϊ�������в���
        ''' </remarks>
        Public ReadOnly Property key As String
            Get
                Return chunkOffset.GetJson
            End Get
        End Property

        Friend Sub New(chunkOffset As Long())
            Me.chunkOffset = chunkOffset
            Me.hashcode = chunkOffset.GetHashCode
        End Sub

        Public Overrides Function GetHashCode() As Integer
            Return hashcode
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            If Me Is obj Then
                Return True
            ElseIf obj Is Nothing Then
                Return False
            ElseIf GetType(ChunkOffsetKey) IsNot obj.[GetType]() Then
                Return False
            End If

            Dim other As ChunkOffsetKey = DirectCast(obj, ChunkOffsetKey)

            Return chunkOffset.SequenceEqual(other.chunkOffset)
        End Function

        Public Overrides Function ToString() As String
            Return "ChunkOffsetKey [chunkOffset=" & chunkOffset.GetJson & ", hashcode=" & hashcode & "]"
        End Function

    End Class
End Namespace
