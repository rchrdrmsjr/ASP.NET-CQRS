﻿namespace todoApi.CQRS.DTOs
{
    public class TodoDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
