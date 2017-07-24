# binary-slice-file-reader
C# reader for 3DSIM binary scan files

# Usage
```
dotnet run <scanfile.bin> <scanFileSummary>
```

# File Format
## Introduction
The binary file is made up of a series of data blocks where each data block looks like this:

| Data          | Type       | Notes                                                      |
|---------------|------------|------------------------------------------------------------|
| Block Type Id | int        | ID of one of the block types described in the next section |
| Block Size    | int        | Number of bytes in the Data section                        |
| Data          | byte array | Data - "block size" bytes                                  |

## Block Types

### Block Type 1 - File Version
Data will contain n ascii bytes to make a string

### Block Type 2 - Layer
| Data          | Type       | Notes                                                      |
|---------------|------------|------------------------------------------------------------|
| Layer Index   | int        | Slice layer                                                |

### Block Type 3 - Contours
|   | Data          | Type       | Notes                                                      |
|---|---------------|------------|------------------------------------------------------------|
|   | Count         | int        | Number of contours                                         |
| X | Type          | int        | 0 - outside, 1 - inside, 2 - open                          |
| X | Points        | int        | Number of points in the contour                            |
| X | Point Data    | byte array | x1,y1,x2,y2,x3,y3…                                         |

X Repeat count times - 1 per contour

### Block Type 4 - Parameter Sets
|   | Data          | Type       | Notes                                                      |
|---|---------------|------------|------------------------------------------------------------|
|   | Count         | int        | Number of Parameter Sets                                   |
| X | Id            | int        | Unique identifier                                          |
| X | Type          | int        | 0 - fill, 1 - contour, … more to come                      |
| X | Laser Power   | int        | Watts (w)                                                  |
| X | Laser Speed   | float      | meters per second (m/s)                                    |

X Repeat count times - 1 per parameter set

### Block Type 5 - Scan Lines
| Data             | Type       | Notes                                                      |
|------------------|------------|------------------------------------------------------------|
| Count            | int        | Number of scan lines                                       |
| Parameter Set Id | int        | Number of bytes in the Data section                        |
| Scan line data   | byte array | (line1) x1,y1,x2,y2 (line2) x1,y1,x2,y2 …                  |

