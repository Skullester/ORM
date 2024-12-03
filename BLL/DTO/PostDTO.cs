﻿namespace BLL.DTO;

public class PostDTO : IElementDTO
{
    public int Id { get; set; }
    public string Name { get; set; }

    public PostDTO(string name)
    {
        Name = name;
    }
}