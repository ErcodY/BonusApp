using Base;

using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Collections.Generic;

namespace Application.Services;

public class GroupJsonService: IJsonService
{
    public string FilePath { get; init; }
    
    public GroupJsonService(string filePath)
    {
        FilePath = filePath;
    }
    
    public void Save<T>(IEnumerable<T> items) where T : IModel
    {
        var json = JsonSerializer.Serialize(items);
        File.WriteAllText(FilePath, json);
    }

    public IEnumerable<T> Load<T>() where T : IModel
    {
        var json = File.ReadAllText(FilePath);
        IEnumerable<T>? groups;
        try
        {
            groups = JsonSerializer.Deserialize<IEnumerable<T>>(json);
        }
        catch (ArgumentNullException e)
        {
            throw new ArgumentNullException($"Error while json deserialize in {MethodBase.GetCurrentMethod()!.Name}", e);
        }
        catch (JsonException e)
        {
            throw new JsonException($"Error while json deserialize in {MethodBase.GetCurrentMethod()!.Name}", e);
        }

        if(groups is not null)
            return groups;
        throw new NotSupportedException($"Something get wrong while json deserialize in {MethodBase.GetCurrentMethod()!.Name}");
    }
}