﻿namespace POS.Application.DTOs.Category.Response
{
    public class CategoryResponseDTO
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int AuditCreateUser { get; set; }
        public int State { get; set; }
        public string? StateCategory { get; set; }
    }
}
