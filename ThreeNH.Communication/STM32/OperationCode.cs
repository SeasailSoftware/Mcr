namespace ThreeNH.Communication.STM32
{

    public enum OperationCode
    {
        InvalidOperation,
        WriteFlash,
        ReadFlash,
        WriteResource,
        WriteFireware,
        WriteFile,
        ReadFile,
        ListFileNames,
        DeleteFile,
        WriteLogo,
    }
}
