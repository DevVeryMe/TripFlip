﻿using System;

namespace TripFlip.Services.Dto.ItemListDtos
{
    public class ResultItemListDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public int RouteId { get; set; }
    }
}
