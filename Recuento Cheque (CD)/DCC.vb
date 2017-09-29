Option Strict Off
Option Explicit On
Module DCC
    '//*******************************************************************************
    '// Copyright © 1995-2006 Digital Check Corporation &
    '// Precision Software Technologies, Inc.
    '// For use by licensed DCC API developers.
    '// You may modify and distribute this code as you see fit, in it's compiled form.
    '// You may NOT distibute any source code without written consent from DCC or PST.
    '// See BUICAPI.H for more information.
    '//*******************************************************************************

    '// Constants for Demo
    '// Output format
    Public Const DEMO_TIF As Short = 1
    Public Const DEMO_BMP As Short = 2
    Public Const DEMO_BMP_TIF As Short = 3
    Public Const DEMO_JPG As Short = 4
    Public Const DEMO_JPG_TIF As Short = 5
    Public Const DEMO_TIF_MEMORY As Short = 6

    '// Edge detection
    Public Const DEMO_EDGE_FRONT As Integer = 1 + 2 + 16 + 64
    Public Const DEMO_EDGE_BACK As Decimal = 1 + 2 + 4 + 16 + 64
    Public Const DEMO_EDGE_THRESH As Short = 600

    '// Function Declares
    Public Declare Function BUICInit Lib "BUICAP32.DLL" () As Integer '// 02-18-97
    Public Declare Function BUICInitPath Lib "BUICAP32.DLL" (ByVal sDLLPath As String, ByVal sININame As String, ByVal sCfgFileName As String) As Integer '// 02-18-97
    Public Declare Function BUICExit Lib "BUICAP32.DLL" () As Integer '// 02-18-97
    Public Declare Function BUICDebug Lib "BUICAP32.DLL" (ByVal iState As Integer) As Integer '// 02-18-97
    Public Declare Function BUICDisplayAbout Lib "BUICAP32.DLL" () As Integer '// 02-18-97
    Public Declare Function BUICStatus Lib "BUICAP32.DLL" () As Integer '// 02-18-97
    Public Declare Function BUICDisplayStatus Lib "BUICAP32.DLL" () As Integer '// 02-18-97
    Public Declare Function BUICSetParam Lib "BUICAP32.DLL" (ByVal iParam As Integer, ByVal iValue As Integer) As Integer '// 02-18-97
    Public Declare Function BUICGetParam Lib "BUICAP32.DLL" (ByVal iParam As Integer) As Integer '// 02-18-97
    Public Declare Function BUICReadConfig Lib "BUICAP32.DLL" (ByVal lpFilename As String) As Integer '// 02-18-97
    Public Declare Function BUICWriteConfig Lib "BUICAP32.DLL" (ByVal lpFilename As String) As Integer '// 02-18-97

    Public Declare Function CheckEndorsementInvert Lib "BUICAP32.DLL" (ByVal iValue As Integer) As Integer '// JOA

    Public Declare Function BUICScan Lib "BUICAP32.DLL" (ByVal iJob As Integer, ByVal lpFront As String, ByVal strFLen As String, ByVal lpBack As String, ByVal strBLen As String, ByVal LpCode As String, ByVal LpLen As String) As Integer '// 02-18-97
    Public Declare Function BUICScanGray Lib "BUICAP32.DLL" (ByVal iJob As Integer, ByVal lpFront As String, ByVal strFLen As String, ByVal lpBack As String, ByVal strBLen As String, ByVal LpCode As String, ByVal LpLen As String, ByVal iImageFmt As Integer) As Integer '// 02-18-97
    Public Declare Function BUICScanMemory Lib "BUICAP32.DLL" (ByVal iJob As Integer, ByVal lpFront As Integer, ByVal strFLen As String, ByVal lpBack As Integer, ByVal strBLen As String, ByVal LpCode As String, ByVal LpLen As String) As Integer '// 02-18-97
    Public Declare Function BUICScanMemoryGray Lib "BUICAP32.DLL" (ByVal iJob As Integer, ByVal lpFront As Integer, ByVal strFLen As String, ByVal lpBack As Integer, ByVal strBLen As String, ByVal LpCode As String, ByVal LpLen As String, ByVal iImageFmt As Integer) As Integer '// 02-18-97

    Public Declare Function BUICCompressImage Lib "BUICAP32.DLL" (ByVal psFilename As String, ByVal pchugeImage As Integer) As Integer '// 02-18-97
    Public Declare Function BUICCompressImageGray Lib "BUICAP32.DLL" (ByVal psFilename As String, ByVal pchugeImage As Integer) As Integer '// 02-18-97

    Public Declare Function funcFindBUICScanner Lib "BUICAP32.DLL" (ByVal wNumSCSIAdapters As Object, ByRef pbAdapter As Byte, ByRef pbTargetID As Byte) As Integer '// 07-17-97

    Public Declare Function BUICStartImageWindow Lib "BUICAP32.DLL" (ByVal hWnd As Integer, ByVal iWindow As Integer) As Integer '// 02-18-97
    Public Declare Function BUICCloseImageWindow Lib "BUICAP32.DLL" (ByVal iWindow As Integer) As Integer '// 02-18-97
    Public Declare Function BUICSizeImageWindow Lib "BUICAP32.DLL" (ByVal iWindow As Integer) As Integer '// 02-18-97
    Public Declare Function BUICSetImageMultPage Lib "BUICAP32.DLL" (ByVal sFile As String, ByVal iPage As Integer, ByVal iWindow As Integer) As Integer '// 02-18-97

    Public Declare Function BUICCopyFile Lib "BUICAP32.DLL" (ByVal InputFile As String, ByVal OutputFile As String) As Integer '// 02-18-97
    Public Declare Function BUICCombineTIFFS Lib "BUICAP32.DLL" (ByVal InputFile As String, ByVal OutputFile As String) As Integer '// 02-18-97
    Public Declare Function BUICCropFile Lib "BUICAP32.DLL" (ByVal InputFile As String, ByVal OutpoutFile As String, ByVal Margin As Short) As Integer '// 02-18-97
    Public Declare Function BUICGetTiffPages Lib "BUICAP32.DLL" (ByVal InputFile As String) As Integer '// 02-18-97
    Public Declare Function BUICGetMicrLine Lib "BUICAP32.DLL" (ByVal InputFile As String, ByVal iPage As Short, ByVal MicrLine As String, ByVal MicrLineSize As Short) As Integer '// 02-18-97
    Public Declare Function BUICSetMicrLine Lib "BUICAP32.DLL" (ByVal InputFile As String, ByVal iPage As Short, ByVal MicrLine As String, ByVal MicrLineSize As Short, ByVal OutputFile As String) As Integer '// 02-18-97
    Public Declare Function BUICUpdateMicrLine Lib "BUICAP32.DLL" (ByVal InputFile As String, ByVal MicrLine As String, ByVal MicrLineSize As Short) As Integer '// 02-18-97

    Public Declare Function BUICGetScannerInfo Lib "BUICAP32.DLL" (ByRef pbAdapter As Byte, ByRef pbTargetID As Byte, ByVal pcVendor As String, ByVal pcProduct As String, ByVal pcRelease As String, ByVal iMessageBox As Integer) As Integer '// 07-17-97
    Public Declare Function GetScannerType Lib "BUICAP32.DLL" () As Integer '// 01-01-02 JB

    Public Declare Function DCCAPIVersion Lib "BUICAP32.DLL" () As Integer '// 01-30-02 JB
    Public Declare Function DCCAPISupportedScanners Lib "BUICAP32.DLL" () As Integer '// 01-30-02 JB
    Public Declare Function BUICGetScannerSerialNumber Lib "BUICAP32.DLL" (ByVal pcSerialNumber As String, ByRef puiDocumentNumber As Integer, ByRef puiTime As Integer) As Integer '// 02-22-02 JB

    'UPGRADE_NOTE: InputString was upgraded to InputString_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    Public Declare Function funcTS400SetPrint Lib "BUICAP32.DLL" (ByVal iParameter As Integer, ByVal iInputValue As Integer, ByVal InputString_Renamed As String) As Integer '// 02-18-97
    Public Declare Function funcTS400SetSorter Lib "BUICAP32.DLL" (ByVal iSorterAlgorithm As Integer, ByVal pbSortString1 As String, ByVal pbSortString2 As String) As Integer '// 02-18-97
    Public Declare Function funcTS400SetLoadFont Lib "BUICAP32.DLL" (ByVal iParameter As Integer, ByVal iInputValue As Integer, ByVal FontFile As String) As Integer '// 02-18-97

    Public Declare Function TS400SetPocket Lib "BUICAP32.DLL" (ByVal iPocket As Integer) As Integer '// 01-21-99
    Public Declare Function funcSetPocket Lib "BUICAP32.DLL" (ByVal iPocket As Integer) As Integer '// 01-21-99

    Public Declare Function BUICEjectDocument Lib "BUICAP32.DLL" () As Integer '// 02-18-97

    Public Declare Function funcLoadPrinterFont Lib "BUICAP32.DLL" (ByVal pcPrinterFontFileName As String) As Integer '// 04-08-98
    Public Declare Function funcSendPrinterString Lib "BUICAP32.DLL" (ByVal shPosition As Short, ByVal psString As String, ByVal iMode As Integer) As Integer '// 04-08-98
    Public Declare Function CheckEndorsementStart Lib "BUICAP32.DLL" (ByVal iScannerType As Integer, ByVal iBMPLength As Integer) As Integer '// 01-01-02 JB
    Public Declare Function CheckEndorsementEnd Lib "BUICAP32.DLL" (ByVal iScannerType As Integer, ByVal BMPFileName As String) As Integer '// 01-01-02 JB
    Public Declare Function CheckEndorsementText Lib "BUICAP32.DLL" (ByVal iScannerType As Integer, ByVal iTextHeight As Integer, ByVal iXOffset As Integer, ByVal iYOffset As Integer, ByVal iBold As Integer, ByVal iItalic As Integer, ByVal strFont As String, ByVal strText As String) As Integer '// 01-01-02 JB
    Public Declare Function funcTS400ClearPrint Lib "BUICAP32.DLL" (ByVal iStartPosition As Integer, ByVal iEndPosition As Integer) As Integer '// 01-01-02 JB

    Public Declare Function funcSetUpCallBack Lib "BUICAP32.DLL" (ByVal nEvent As Integer, ByVal CallBackFunction As MyFunc) As Integer '// 11-22-00 JB
    Public Declare Function funcTS200GetMICR Lib "BUICAP32.DLL" (ByVal psMICR As String, ByVal iMaxMICR As Integer) As Integer '// 01-01-02 JB

    Public Declare Function funcTS200Update Lib "BUICAP32.DLL" (ByVal iOptions As Integer) As Integer '// 11-22-00 JB

    Public Declare Function funcConvGrayImageEdgeDetectBW Lib "BUICAP32.DLL" (ByVal pcImage As Integer, ByVal pszFileName As String, ByVal iEdgeThreshold As Integer, ByVal iTwoD As Integer) As Integer '// 11-22-00 JB
    Public Declare Function funcConvImageToFile Lib "BUICAP32.DLL" (ByVal pcImage As Integer, ByVal iEdgeThreshold As Integer, ByVal iScale As Integer, ByVal iOutputBits As Integer, ByVal iImageFormat As Integer, ByVal pszFileName As String) As Integer '// 11-22-00 JB

    Public Declare Function funcTS400ScanReadInit Lib "BUICAP32.DLL" () As Integer '// 01-01-02 JB
    Public Declare Function funcTS400ScanRead Lib "BUICAP32.DLL" (ByVal iJob As Integer, ByRef plFrontFileSize As Integer, ByRef plBackFileSize As Integer, ByVal pcCodeLine As String, ByRef piCodeLineSize As Integer, ByVal pcFrontFileName As String, ByVal pcBackFileName As String, ByVal pchOutFrontImage As String, ByVal pchOutBackImage As String, ByVal iMemory As Integer, ByVal iImageFormat As Integer) As Integer '// 01-01-02 JB
    Public Declare Function funcTS400ScanReadExit Lib "BUICAP32.DLL" () As Integer '// 01-01-02 JB
    Public Declare Function funcTS400GetScannerRunInfo Lib "BUICAP32.DLL" (ByRef puiWorkMinutes As Integer, ByRef puiNumDocs As Integer) As Integer '// 01-01-02 JB

    Public Declare Function FindE13BMicr Lib "BUICAP32.DLL" (ByVal szCheckFileName As String, ByVal iPage As Integer, ByVal iRotate As Integer, ByVal szOCRMicr As String, ByVal szOCRConfidence As String) As Integer '// 11-11-02 JB
    Public Declare Function FindE13BMicrMem Lib "BUICAP32.DLL" (ByVal pMem As Integer, ByVal szOCRMicr As String, ByVal szOCRConfidence As String) As Integer '// 11-11-02 JB
    Public Declare Function VerifyMicr Lib "BUICAP32.DLL" (ByVal szMagneticMicr As String, ByVal szOCRMicr As String, ByVal szOCRMicrConfidence As String, ByVal szResults As String, ByVal iMaxMagneticErrors As Integer) As Integer '// 11-11-02 JB
    Public Declare Function LastCheckMicrStatus Lib "BUICAP32.DLL" () As Integer '// 11-11-02 JB

    Public Declare Function BUICSetParamString Lib "BUICAP32.DLL" (ByVal iParameter As Integer, ByVal sString As String) As Integer '// 12-02-04 JB
    'UPGRADE_WARNING: Structure typDocStatus may require marshalling attributes to be passed as an argument in this Declare statement. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1050"'
    Public Declare Function DCCScan Lib "BUICAP32.DLL" (ByVal pszFrontTIFF As String, ByVal pszBackTIFF As String, ByVal pszFrontJPEG As String, ByVal pszBackJPEG As String, ByVal pszMICR As String, ByRef piFinalImageQuality As Integer, ByRef piFinalContrast As Integer, ByRef piDocStatus As typDocStatus) As Integer '// 12-02-04 JB
    Public Declare Function DCCLoadImage Lib "BUICAP32.DLL" (ByVal pszImageName As String, ByVal iPage As Integer, ByVal pcDCCImage As Integer, ByVal iMaxImageSize As Integer) As Integer '// 12-02-04 JB
    Public Declare Function DCCVirtualEndorsement Lib "BUICAP32.DLL" (ByVal pcDCCImage As Integer, ByVal pcVirtualString As String, ByVal iTextHeight As Integer, ByVal iLeft As Integer, ByVal iTop As Integer, ByVal iBold As Integer, ByVal iItalic As Integer, ByVal iRotate As Integer, ByVal iDitherMode As Integer, ByVal iRead As Integer, ByVal iGreenGray As Integer, ByVal iBlue As Integer, ByVal szFont As String) As Integer
    Public Declare Function DCCScanVirtualEndorsement Lib "BUICAP32.DLL" (ByVal pcVirtualString As String, ByVal dwStartPosition As Integer, ByVal dwMode As Integer, ByVal iStartCounter As Integer, ByVal iIncrement As Integer, ByVal iTextHeight As Integer, ByVal iLeft As Integer, ByVal iTop As Integer, ByVal iBold As Integer, ByVal iItalic As Integer, ByVal iRotate As Integer, ByVal iDitherMode As Integer, ByVal iRead As Integer, ByVal iGreenGray As Integer, ByVal iBlue As Integer, ByVal szFont As String) As Integer


    '// WinAPI Declares
    Public Declare Sub Sleep Lib "kernel32.dll" (ByVal dwMilliseconds As Integer)

    '// Memory Header
    Public Structure BUICMemHdr
        Dim hdrImageWidth As Short '// image width in pixels
        Dim hdrImageLength As Short '// image length in pixels
        Dim hdrBitsPixel As Short '// Bits per pixel
        Dim hdrDPI As Short '// DPI used while reading image
    End Structure

    '// Doc status type
    Public Structure typDocStatus
        Dim MaxEdgeCrop As Integer
        Dim SpecklesRemoved As Integer
        Dim StandardCkSize As Integer
        Dim Skew As Integer
        Dim BentLeftTopPix As Integer
        Dim BentRightTopPix As Integer
        Dim BentLeftBottomPix As Integer
        Dim BentRightBottomPix As Integer
        Dim FirstSolidLine As Integer
        Dim MICRQualityStatus As Integer
        Dim PctBlackBits As Integer
        Dim CompressedImgSize As Integer
        Dim CARPresent As Integer
        Dim LARPresent As Integer
        Dim PayeePresent As Integer
        Dim DatePresent As Integer
        Dim SignaturePresent As Integer
        Dim MemoPresent As Integer
        Dim BLOBPresent As Integer
        Dim CarbonPresent As Integer
        Dim StreakStatus As Integer
        Dim GrayScaleContrast As Integer
        Dim ImageFocus As Integer
        Dim DocInfo As Integer
        <VBFixedString(32), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=32)> Public Reserved As String
    End Structure

    '// Parameters for funcSetUpCallBack
    Public Const TS200_CB_EVENT_SCAN As Short = &H4S
    Public Const TS200_CB_EVENT_MICR As Integer = &H100000
    Public Const TS200_CB_EVENT_FRONT As Integer = &H200000
    Public Const TS200_CB_EVENT_BACK As Integer = &H400000

    '// Device Constants
    Public Const BUIC1000 As Short = 2
    Public Const BUIC1500 As Short = 3
    Public Const TS200 As Short = 200
    Public Const TS300 As Short = 300
    Public Const TS300EBS As Short = 314 '// 300 + 0x0E
    Public Const TS400 As Short = 400

    '// Supported Device Constants
    Public Const SUP_BUIC1000 As Short = &H1S
    Public Const SUP_BUIC1500 As Short = &H2S
    Public Const SUP_TS200 As Short = &H8S
    Public Const SUP_TS300 As Short = &H10S
    Public Const SUP_TS300EBS As Short = &H20S
    Public Const SUP_TS400 As Short = &H4S

    '// JobType/Sides
    Public Const BUIC_SCAN_FRONT As Short = 1
    Public Const BUIC_SCAN_BACK As Short = 2
    Public Const BUIC_SCAN_BOTH As Short = 3
    Public Const BUIC_SCAN_MICR As Short = 4
    Public Const BUIC_SCAN_FRONT_MICR As Short = 5
    Public Const BUIC_SCAN_BACK_MICR As Short = 6
    Public Const BUIC_SCAN_BOTH_MICR As Short = 7
    Public Const BUIC_SCAN_NONE_CODE As Short = BUIC_SCAN_MICR
    Public Const BUIC_SCAN_FRONT_CODE As Short = BUIC_SCAN_FRONT_MICR
    Public Const BUIC_SCAN_BACK_CODE As Short = BUIC_SCAN_BACK_MICR
    Public Const BUIC_SCAN_BOTH_CODE As Short = BUIC_SCAN_BOTH_MICR

    Public Const BUIC_DEBUG_OFF As Short = 0
    Public Const BUIC_DEBUG_ON As Short = 1

    Public Const MAX_CFG_PARAMS As Short = 120 '// Maximum numeric parameter number
    Public Const MAX_CFG_STRING_PARAMS As Short = 10 '// Maximum string parameter number
    Public Const MAX_PARAMS As Short = MAX_CFG_PARAMS
    Public Const BUIC_DEV_OFF As Short = 0
    Public Const BUIC_DEV_ON As Short = 1
    Public Const CFG_DEV_OFF As Short = 0
    Public Const CFG_DEV_ON As Short = 1

    '// Two Color Light Parameters
    Public Const CFG_FRONT_RIDO_ENABLE As Short = 61
    Public Const CFG_FRONT_OUT_FORMAT As Short = 62
    Public Const CFG_FRONT_INTENSITY As Short = 63
    Public Const CFG_FRONT_CLIP_THRESH As Short = 64
    Public Const CFG_FRONT_BW_THRESH As Short = 65
    Public Const CFG_BACK_RIDO_ENABLE As Short = 66
    Public Const CFG_BACK_OUT_FORMAT As Short = 67
    Public Const CFG_BACK_INTENSITY As Short = 68
    Public Const CFG_BACK_CLIP_THRESH As Short = 69
    Public Const CFG_BACK_BW_THRESH As Short = 70

    '// Devices Parameters
    Public Const CFG_DEV_DSP As Short = 55
    Public Const TPARAM_DEV_DSP As Short = CFG_DEV_DSP
    Public Const CFG_DEV_EXT_READER As Short = 47
    Public Const CFG_DEV_BADGE_READER As Short = 3
    Public Const BPARAM_BADGE As Short = CFG_DEV_BADGE_READER '// not used
    Public Const CFG_DEV_ENDORSER As Short = 4
    Public Const BPARAM_ENDORSER As Short = CFG_DEV_ENDORSER '// Optional Capability
    Public Const CFG_DEV_SORTER As Short = 32
    Public Const BPARAM_SORTER As Short = CFG_DEV_SORTER '// Optional Capability
    Public Const CFG_DEV_PRINTER As Short = 31
    Public Const BPARAM_PRINTER As Short = CFG_DEV_PRINTER
    Public Const CFG_DEV_DOUBLE_FEED As Short = 7
    Public Const TPARAM_DOUBLEFEED_THICK As Short = CFG_DEV_DOUBLE_FEED
    Public Const BPARAM_PHOTOCELL As Short = CFG_DEV_DOUBLE_FEED

    '// MICR Parameters
    Public Const CFG_MICR_ENABLE As Short = 1
    Public Const BPARAM_MAGNREADER As Short = CFG_MICR_ENABLE '// BUIC1500 and TS400
    Public Const CFG_MICR_FONT As Short = 2
    Public Const BPARAM_MAGNTYPE As Short = CFG_MICR_FONT
    Public Const CFG_MICR_CMC7 As Short = 0
    Public Const CFG_MICR_E13B As Short = 1
    Public Const BUIC_CMC7 As Short = CFG_MICR_CMC7
    Public Const BUIC_E13B As Short = CFG_MICR_E13B
    Public Const CFG_MICR_INITIAL_POS As Short = 14
    Public Const CFG_MICR_END_POS As Short = 71

    '// AUX.READER Parameters
    Public Const CFG_AUXREADER_TYPE As Short = 72
    Public Const CFG_AUXREADER_FONT As Short = 73
    Public Const CFG_AUXREADER_INITIALPOS As Short = 74
    Public Const CFG_AUXREADER_ENDPOS As Short = 75
    Public Const CFG_AUXREADER_BAUDRATE As Short = 76
    Public Const CFG_AUXREADER_PARITY As Short = 77
    Public Const CFG_AUXREADER_CHARLENGTH As Short = 78
    Public Const CFG_AUXREADER_STOPBIT As Short = 79

    '// SORTER Parameters
    Public Const CFG_SORTER_INPUT As Short = 49
    Public Const CFG_SORTER_ALGORITHM As Short = 39
    Public Const BUIC_SORTER_ALGORITHM As Short = CFG_SORTER_ALGORITHM
    '// Algorithm Options
    Public Const CFG_SORTER_ALG_NONE As Short = 0 '// NONE
    Public Const CFG_SORTER_ALG_PRESENT As Short = 1 '// PRESENT OR VALID MICR
    Public Const CFG_SORTER_ALG_EQUAL As Short = 2 '// MICR == String1
    Public Const CFG_SORTER_ALG_NOT_EQUAL As Short = 3 '// MICR != String1
    Public Const CFG_SORTER_ALG_GT_EQUAL As Short = 4 '// MICR >= String1
    Public Const CFG_SORTER_ALG_LT_EQUAL As Short = 5 '// MICR <= String1
    Public Const CFG_SORTER_ALG_BETWEEN As Short = 6 '// (MICR >= String1) && (MICR <= String2)
    Public Const CFG_SORTER_ALG_OUTSIDE As Short = 7 '// (MICR <= String1 (MICR >= String2)
    Public Const CFG_SORTER_ALG_EQUAL1OR2 As Short = 8 '// (MICR == String1) || (MICR == String2)
    Public Const CFG_SORTER_ALG_NOTEQU1OR2 As Short = 9 '// (MICR != String1) (MICR != String2)
    Public Const BUIC_SORTER_ALG_NONE As Short = CFG_SORTER_ALG_NONE '// NONE
    Public Const BUIC_SORTER_ALG_PRESENT As Short = CFG_SORTER_ALG_PRESENT '// PRESENT OR VALID MICR
    Public Const BUIC_SORTER_ALG_EQUAL As Short = CFG_SORTER_ALG_EQUAL '// MICR == String1
    Public Const BUIC_SORTER_ALG_NOT_EQUAL As Short = CFG_SORTER_ALG_NOT_EQUAL '// MICR != String1
    Public Const BUIC_SORTER_ALG_GT_EQUAL As Short = CFG_SORTER_ALG_GT_EQUAL '// MICR >= String1
    Public Const BUIC_SORTER_ALG_LT_EQUAL As Short = CFG_SORTER_ALG_LT_EQUAL '// MICR <= String1
    Public Const BUIC_SORTER_ALG_BETWEEN As Short = CFG_SORTER_ALG_BETWEEN '// (MICR >= String1) && (MICR <= String2)
    Public Const BUIC_SORTER_ALG_OUTSIDE As Short = CFG_SORTER_ALG_OUTSIDE '// (MICR <= String1 (MICR >= String2)
    Public Const BUIC_SORTER_ALG_EQUAL1OR2 As Short = CFG_SORTER_ALG_EQUAL1OR2 '// (MICR == String1) || (MICR == String2)
    Public Const BUIC_SORTER_ALG_NOTEQU1OR2 As Short = CFG_SORTER_ALG_NOTEQU1OR2 '// (MICR != String1) (MICR != String2)
    Public Const CFG_SORTER_INITSTRINGPOS As Short = 80
    Public Const CFG_SORTER_ENDSTRINGPOS As Short = 81
    Public Const CFG_SORTER_EJECTPOCKET As Short = 82
    Public Const CFG_SORTER_STRING1 As Short = 40
    Public Const TBPARAM_SORTER_STRING1 As Short = CFG_SORTER_STRING1 '// Teller TS400 and BUIC1500 ONLY
    Public Const CFG_SORTER_STRING2 As Short = 41
    Public Const TBPARAM_SORTER_STRING2 As Short = CFG_SORTER_STRING2 '// Teller TS400 and BUIC1500 ONLY

    '// IJ Printer Parameters
    Public Const CFG_IJPRINTER_PRINTBMP As Short = 33
    Public Const TBPARAM_PRINT_FILE As Short = CFG_IJPRINTER_PRINTBMP '// Teller TS400 and BUIC1500 ONLY
    Public Const CFG_IJPRINTER_BMPFILENAME As Short = 35
    Public Const TBPARAM_PRINT_FILENAME As Short = CFG_IJPRINTER_BMPFILENAME '// Teller TS400 and BUIC1500 ONLY
    Public Const CFG_IJPRINTER_FONTFILENAME As Short = 83
    Public Const CFG_IJPRINTER_INITIALPOS As Short = 84
    Public Const CFG_IJPRINTER_SELECTION As Short = 85
    Public Const CFG_IJPRINTER_INTENSITY As Short = 86
    Public Const CFG_IJPRINTER_XPIXELD As Short = 87
    Public Const CFG_IJPRINTER_CLEAN44 As Short = 88
    Public Const CFG_IJPRINTER_CLEAN18 As Short = 89
    Public Const CFG_IJPRINTER_CLEAN6 As Short = 90
    Public Const CFG_IJPRINTER_CLEAN2 As Short = 91
    Public Const CFG_IJPRINTER_BMP12FILENAME As Short = 112
    Public Const CFG_IJPRINTER_FONT12FILENAME As Short = 113
    Public Const CFG_IJPRINTER_PRINTPOS As Short = 114

    '// Image Parameters
    Public Const CFG_IMAGE_FORMAT As Short = 92
    Public Const CFG_BMP_VERTICAL As Short = 1
    Public Const CFG_BMP_HORIZONTAL As Short = 2
    Public Const CFG_UNCOMP_TIFF_VERTICAL As Short = 3
    Public Const CFG_UNCOMP_TIFF_HORIZONTAL As Short = 4
    Public Const CFG_JPEG_VERTICAL As Short = 5
    Public Const CFG_JPEG_HORIZONTAL As Short = 6
    Public Const CFG_COMP_TIFF_VERTICAL As Short = 7
    Public Const CFG_COMP_TIFF_HORIZONTAL As Short = 8
    Public Const FORMAT_TIFF_UNCOMP As Short = 0
    Public Const FORMAT_TIFF As Short = 1
    Public Const FORMAT_BMP As Short = 3 '// Gray scale stored to BMP
    Public Const FORMAT_JPEGG As Short = 4 '// Gray scale compressed to JPEG Gray (one component)
    Public Const CFG_IMAGE_FRONTONLY As Short = 42
    Public Const TBPARAM_ACQ_FRONTONLY As Short = CFG_IMAGE_FRONTONLY
    Public Const CFG_IMAGE_FRONTBACK As Short = 43
    Public Const TBPARAM_ACQ_FRONTBACK As Short = CFG_IMAGE_FRONTBACK
    Public Const CFG_IMAGE_BW As Short = 23
    Public Const CFG_IMAGE_BW_BW As Short = 0
    Public Const CFG_IMAGE_BW_GRAY As Short = 1
    Public Const BPARAM_GRAY As Short = CFG_IMAGE_BW
    Public Const BUIC_MONO As Short = CFG_IMAGE_BW_BW
    Public Const BUIC_GRAY As Short = CFG_IMAGE_BW_GRAY
    Public Const TBPARAM_ACQ_BLACKWHITE As Short = CFG_IMAGE_BW '// Teller TS400 and BUIC1500 ONLY
    Public Const BUIC_BLACK As Short = CFG_IMAGE_BW_BW
    Public Const CFG_IMAGE_GRAY256LEVEL As Short = 45
    Public Const CFG_IMAGE_GRAY256LEVEL_16 As Short = 0
    Public Const CFG_IMAGE_GRAY256LEVEL_256 As Short = 1
    Public Const TBPARAM_ACQ_256GRAY As Short = CFG_IMAGE_GRAY256LEVEL '// Teller TS400 and BUIC1500 ONLY
    Public Const BUIC_GRAY4 As Short = CFG_IMAGE_GRAY256LEVEL_16
    Public Const BUIC_GRAY8 As Short = CFG_IMAGE_GRAY256LEVEL_256
    Public Const CFG_IMAGE_RESOLUTION As Short = 6
    Public Const CFG_IMAGE_RESOL_100X100 As Short = 0
    Public Const CFG_IMAGE_RESOL_200X100 As Short = 3
    Public Const CFG_IMAGE_RESOL_200X200 As Short = 1
    Public Const CFG_IMAGE_RESOL_200X100RAW As Short = 2
    Public Const BPARAM_DPI As Short = CFG_IMAGE_RESOLUTION
    Public Const BUIC_DPI100 As Short = CFG_IMAGE_RESOL_100X100
    Public Const BUIC_DPI200 As Short = CFG_IMAGE_RESOL_200X200
    Public Const BUIC_DPI200X100FAST As Short = CFG_IMAGE_RESOL_200X100 '// ONLY FOR Teller TS400 and BUIC1500
    Public Const BUIC_DPI200X100 As Short = CFG_IMAGE_RESOL_200X100 '// ONLY FOR Teller TS400 and BUIC1500
    Public Const CFG_IMAGE_FRONT_BW_THRESH As Short = 11 '// Range from 0 to 15
    Public Const BPARAM_FRONTTHRESHOLD As Short = CFG_IMAGE_FRONT_BW_THRESH '// 0 to 14, start with 8 to 10.
    Public Const CFG_IMAGE_BACK_BW_THRESH As Short = 12 '// Range from 0 to 15
    Public Const BPARAM_REARTHRESHOLD As Short = CFG_IMAGE_BACK_BW_THRESH '// 0 to 14, start with 8 to 10.

    '// CLIP AREA Parameters
    Public Const CFG_CLIPAREA_IMGRIGHT As Short = 19
    Public Const BPARAM_WINDOWX1 As Short = CFG_CLIPAREA_IMGRIGHT '// ACQ: Img Right Teller TS400,Buic1500
    Public Const CFG_CLIPAREA_IMGLEFT As Short = 21
    Public Const BPARAM_WINDOWX2 As Short = CFG_CLIPAREA_IMGLEFT '// ACQ: Img Right Teller TS400,Buic1500
    Public Const CFG_CLIPAREA_IMGBOTTOM As Short = 20
    Public Const BPARAM_WINDOWY1 As Short = CFG_CLIPAREA_IMGBOTTOM '// ACQ: Img Bottom Teller TS400,Buic1500
    Public Const CFG_CLIPAREA_IMGTOP As Short = 22
    Public Const BPARAM_WINDOWY2 As Short = CFG_CLIPAREA_IMGTOP '// ACQ: Img Bottom Teller TS400,Buic1500

    '// RS232 Parameters
    Public Const CFG_RS232_MODE As Short = 93
    Public Const CFG_RS232_BAUDRATE As Short = 94
    Public Const CFG_RS232_PARITY As Short = 95
    Public Const CFG_RS232_CHARLENGTH As Short = 96
    Public Const CFG_RS232_STOPBIT As Short = 97

    '// MISCELLANEOUS Parameters
    Public Const CFG_MISC_DOUBLEFEEDLEN As Short = 98
    Public Const CFG_MISC_MANUALFEED As Short = 51
    Public Const TBPARAM_MANUALFEED As Short = CFG_MISC_MANUALFEED

    '// DLL Config Parameters
    Public Const CFG_DLLCONFIG_DLL_VERBOSE As Short = 99
    Public Const CFG_DLLCONFIG_MEMORYDIM As Short = 100

    '// IMAGE CROPPING Parameters
    Public Const CFG_IMAGECROPPING_ENABLE As Short = 36
    Public Const TBPARAM_IMGCROP As Short = CFG_IMAGECROPPING_ENABLE '// Teller TS400 and BUIC1500 ONLY

    '// SCSI
    Public Const CFG_SCSI_ADAPTERS As Short = 101
    Public Const CFG_SCSI_TARGETID As Short = 102
    Public Const CFG_SCSI_ADAPTERID As Short = 103

    Public Const CFG_SETUP_FORMAT As Short = 54
    Public Const CFG_SETUP_ROTATION As Short = 37
    Public Const TBPARAM_IMGROTATE As Short = CFG_SETUP_ROTATION '// Teller TS400 and BUIC1500 ONLY
    Public Const CFG_SETUP_FILENAMETERM As Short = 104
    Public Const CFG_SETUP_TIFFMICRTAG As Short = 105
    Public Const CFG_SETUP_BMPLOADONCE As Short = 106
    Public Const CFG_SETUP_MICR_FORMAT As Short = 107
    Public Const CFG_ORIGINAL_MICR As Short = 0
    Public Const CFG_LESS_E_MICR As Short = 1
    Public Const CFG_SPACES_MICR As Short = 2
    Public Const CFG_IMAGECROPPING_THRESH As Short = 108
    Public Const CFG_MISC_IMAGEWAIT As Short = 109
    Public Const CFG_SETUP_PHOTODELAY As Short = 24
    Public Const BPARAM_PHOTODELAY As Short = CFG_SETUP_PHOTODELAY
    Public Const CFG_SETUP_DOC_THICKNESS As Short = 50
    Public Const TBPARAM_ACQ_DOCTHICKNESS As Short = CFG_SETUP_DOC_THICKNESS
    Public Const CFG_MISC_JPEG_QUALITY As Short = 110
    Public Const TPARAM_JPEG_QUALITY As Short = CFG_MISC_JPEG_QUALITY
    Public Const CFG_MISC_ENABLE_SCAN As Short = 111
    Public Const TPARAM_ENABLE_SCAN As Short = CFG_MISC_ENABLE_SCAN

    Public Const CFG_IJPRINTER_PRINTSTRING As Short = 34 '// Teller TS400 and BUIC1500 ONLY
    Public Const TBPARAM_PRINT_STRING As Short = CFG_IJPRINTER_PRINTSTRING '// Teller TS400 and BUIC1500 ONLY
    Public Const TBPARAM_REWINDDELAY As Short = 38 '// Teller TS400 and BUIC1500 ONLY

    '// SetParamString Parameters
    Public Const CFG_INIPATH As Short = 135
    Public Const CFG_CFGPATH As Short = 136
    Public Const CFG_DLLPATH As Short = 137
    Public Const CFG_FIRMWAREPATH As Short = 138

    Public Const BPARAM_CCDMODE As Short = 5
    Public Const BUIC_ACQ_CCD_1MHZ As Short = 0
    Public Const BUIC_ACQ_CCD_2MHZ As Short = 1

    Public Const BPARAM_PHOTO As Short = 8
    Public Const BPARAM_FRONTACQSTART As Short = 9
    Public Const BPARAM_REARACQSTART As Short = 10
    Public Const BUIC_THRESH_MIN As Short = 0
    Public Const BUIC_THRESH_MAX As Short = 14
    Public Const BPARAM_ACQLENGTH As Short = 13
    Public Const BPARAM_STARTACQMAG As Short = CFG_MICR_INITIAL_POS
    Public Const BPARAM_STARTENDORSER As Short = 15
    Public Const BPARAM_STOPMOTOR As Short = 16
    Public Const BPARAM_SCANNERID As Short = 17
    Public Const BPARAM_DOCHEIGHT As Short = 18

    Public Const TBPARAM_DEV_INTERMICR As Short = 46
    Public Const TBPARAM_EXTREADER_TYPE As Short = 47
    Public Const BUIC_OCR As Short = 0
    Public Const BUIC_MICR As Short = 1
    Public Const TBPARAM_MICR_FONT As Short = 48
    Public Const TBPARAM_SORTER_INPUT As Short = 49
    Public Const BUIC_INT As Short = 0
    Public Const BUIC_EXT As Short = 1
    Public Const TBPARAM_FORTHANDBACK As Short = 52
    Public Const TBPARAM_KIOSKMODE As Short = 53
    Public Const TBPARAM_IMGFORMAT As Short = 54
    Public Const TPARAM_MICR_ENABLE As Short = 56
    Public Const CFG_MICR_FORMAT As Short = 57
    Public Const TPARAM_MICR_FORMAT As Short = CFG_MICR_FORMAT
    Public Const MICR_FORMAT_NOSPACE As Short = 0
    Public Const MICR_FORMAT_ALLSPACES As Short = 1
    Public Const TPARAM_MICR_PROCESSOR As Short = 58
    Public Const TBPARAM_OVERSCAN As Short = 59 '// Teller TS400 ONLY.  Setting overscan for right edge
    Public Const TBPARAM_PRINT_IJSTRING As Short = 60 '// Print IJ string

    Public Const MAX_IMAGE_LENGTH As Short = 5000

    Public Const BUIC_CODELINE_OFF As Short = 0
    Public Const BUIC_CODELINE_ON As Short = 1

    '// MICR OCR Return Constants
    Public Const VERIFIEDNOTFIXED As Short = 1
    Public Const VERIFIEDLEADINGBANK As Short = 2
    Public Const VERIFIEDFIXEDMISSED As Short = 4
    Public Const VERIFIED25FIXED As Short = 8
    Public Const VERIFIEDREVIEW As Short = 16
    Public Const VERIFIEDSAME As Short = 256

    '// Next Section is required for scan to memory
    Private Const DEMO_MEM_SIZE As Integer = 1703936 '// Should be max size (2048 * 832)

    '// Constants for Memory Flags
    Private Const GMEM_FIXED As Short = &H0S
    Private Const GMEM_MOVEABLE As Short = &H2S
    Private Const GMEM_NOCOMPACT As Short = &H10S
    Private Const GMEM_NODISCARD As Short = &H20S
    Private Const GMEM_ZEROINIT As Short = &H40S
    Private Const GMEM_MODIFY As Short = &H80S
    Private Const GMEM_DISCARDABLE As Short = &H100S
    Private Const GMEM_NOT_BANKED As Short = &H1000S
    Private Const GMEM_SHARE As Short = &H2000S
    Private Const GMEM_DDESHARE As Short = &H2000S
    Private Const GMEM_NOTIFY As Short = &H4000S
    Private Const GMEM_LOWER As Short = GMEM_NOT_BANKED

    Private Const GHND As Integer = 66 '(GMEM_MOVEABLE Or GMEM_ZEROINIT)
    Private Const GPTR As Integer = 64 '(GMEM_FIXED Or GMEM_ZEROINIT)

    '// Allocates memory, returns memory handle
    Private Declare Function GlobalAlloc Lib "kernel32" (ByVal wFlags As Integer, ByVal dwBytes As Integer) As Integer
    '// Lock memory, returns memory pointer
    Private Declare Function GlobalLock Lib "kernel32" (ByVal hMem As Integer) As Integer
    '// Unlocks memory, using memory handle
    Private Declare Function GlobalUnlock Lib "kernel32" (ByVal hMem As Integer) As Integer
    '// Frees memory, using memory handle
    Private Declare Function GlobalFree Lib "kernel32" (ByVal hMem As Integer) As Integer

    '// Function: MemoryGet
    '//           Allocates & Locks memory
    '// Returns:  True if successful
    Public Function MemoryGet(ByRef lngMemHwnd As Integer, ByRef lngMemPtr As Integer) As Boolean

        lngMemHwnd = GlobalAlloc(GPTR, DEMO_MEM_SIZE)
        If lngMemHwnd = 0 Then
            Exit Function
        End If

        lngMemPtr = GlobalLock(lngMemHwnd)

        MemoryGet = (lngMemPtr <> 0)

    End Function

    '// Function: MemoryRelease
    '//           Releases previously allocated memory
    '// Returns:  True
    Public Function MemoryRelease(ByRef lngMemHwnd As Integer) As Boolean

        If lngMemHwnd = 0 Then Exit Function

        '// Free Memory
        GlobalUnlock(lngMemHwnd)
        GlobalFree(lngMemHwnd)

        MemoryRelease = True

    End Function

    Public Delegate Function MyFunc(ByVal dwReason As Integer) As Integer

    Public Function MICRCallBack(ByVal dwReason As Integer) As Integer

        Dim ImgSrt As Integer
        Dim bandamag As String = Space(100)

        funcTS200GetMICR(bandamag, 100)

        If (Sort1) Then
            If (BatchSort = Math.Floor(BatchSort / 2) * 2) Then
                TS400SetPocket(0)
            Else
                TS400SetPocket(1)
            End If
        End If
        If (Sort2) Then
            If (BatchSort = Math.Floor(BatchSort / 4) * 4) Then
                TS400SetPocket(1)
            Else
                TS400SetPocket(0)
            End If
        End If
        If (Sort3) Then
            ImgSrt = bandamag.IndexOf("?")
            If (ImgSrt >= 0) Then
                TS400SetPocket(1)
            Else
                TS400SetPocket(0)
            End If
        End If

        BatchSort = BatchSort + 1
        Application.DoEvents()
        MICRCallBack = 1
    End Function
End Module
