# binary-slice-file-reader
C# reader for 3DSIM binary scan files

# Usage
```
dotnet run <scanfile.bin> <scanFileSummary>
```

# File Format
## Introduction
The binary file is made up of a series of data blocks where each data block looks like this:

Data	Type	Notes
Block Type ID	Int	ID of one of the block types described in the next section
Block Size	Int	Number of bytes in the Data section
Data	?	Data - <block size> bytes

## Block Types

### Block Type 1 - File Version
Data will contain n ascii bytes to make a string

### Block Type 2 - Layer
	Data	Type	Notes
	Layer Index	Int	Slice layer


### Block Type 3 - Contours
	Data	Type	Notes
	Count	Int	Number of contours
X	Type	Int	0 - outside, 1 - inside, 2 - open
X	Points	Int	Number of points in the contour
X	Point Data	float	x1,y1,x2,y2,x3,y3…
X Repeat count times - 1 per contour

### Block Type 4 - Parameter Sets
	Data	Type	Notes
	Count	Int	Number Parameter Sets
X	ID	Int	Unique identifier
X	Type	Int	0 - fill, 1 - contour, … more to come
X	Laser Power	int	(W)
X	Laser Speed	float	(m/s)
X Repeat count times - 1 per parameter set

### Block Type 5 - Scan Lines
	Data	Type	Notes
	Count	Int	Number of Scan Lines
	Parameter Set ID	Int	ID of parameter set for this group of scan lines
	PointData	float	(line1) x1,y1,x2,y2 (line2) x1,y1,x2,y2 ….

