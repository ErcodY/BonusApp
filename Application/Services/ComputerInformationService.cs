using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

using Application.Tools.Logger;

namespace Application.Services;

/// <summary>
/// ComputerInformationService - Service to get information about the computer
/// Contains name of CPU, amount of RAM, Version of OS and PC Model from BIOS.
/// </summary>
public class ComputerInformationService
{
    private static ComputerInformationService? _instance;
    public static ComputerInformationService Instance
    {
        get => _instance ??= new();
    } 
    
    public OperatingSystem? OperatingSystem { get; private set; }
    public string? Model { get; private set; }
    public string? CpuName { get; private set; }
    public long? RamAmount { get; private set; }
    
    /// <summary>
    /// GetPhysicallyInstalledSystemMemory returns the total amount of physical memory installed on the computer, in
    /// kilobytes. GetPhysicallyInstalledSystemMemory is Windows API function
    /// </summary>
    /// <param name="totalMemoryInKilobytes">The total amount of physical memory, in kilobytes.</param>
    [DllImport("kernel32.dll")]
    private static extern bool GetPhysicallyInstalledSystemMemory(out long totalMemoryInKilobytes);

    private ComputerInformationService()
    {
        Init();
    }
    
    /// <summary>
    /// Init initializes the computer information service
    /// </summary>
    private void Init()
    {
        OperatingSystem = Environment.OSVersion;
        try
        {
            CpuName = GetCpu();
            RamAmount = GetRam();
            Model = GetModel();
        }
        catch (NotSupportedException ex)
        {
            Logger.Instance.Log(LogLevel.Error, ex);
        }
    }
    private string GetCpu()
    {
        var registryKey = Registry.LocalMachine;
        /* getting the key of the CPU in the registry*/
        var cpu =  registryKey.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0",
            RegistryKeyPermissionCheck.ReadSubTree);
        
        if (cpu is null)
            throw new NotSupportedException("Error while getting CPU information");
        
        var name = cpu.GetValue("ProcessorNameString")!.ToString();
        
        if (name is null)
            throw new NotSupportedException("Error while getting CPU name");
        
        return name;
    }
    private long GetRam()
    {
        GetPhysicallyInstalledSystemMemory(out var ram);
        /* Converts the amount of ram from kilobytes to gigabytes. */
        return ram / 1024 / 1024;
    }
    private string GetModel()
    {
        /* Getting the model of the computer. */
        var registryKey = Registry.LocalMachine;
        /* getting the key of the ModelName in the registry from the BIOS */
        var model =  registryKey.OpenSubKey(@"Hardware\Description\System\BIOS",
            RegistryKeyPermissionCheck.ReadSubTree);
        
        if (model is null)
            throw new NotSupportedException("Error while getting Model information");
        
        var name = model.GetValue("SystemProductName")!.ToString();
        
        if (name is null)
            throw new NotSupportedException("Error while getting Model name");
        
        return name;
    }
    
}