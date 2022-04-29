using Base;

namespace Application.Models;

public class Photo: IModel
{
    public int Id { get; set; }   
    public string Path { get; set; }
    public string Alt { get; set; }
}