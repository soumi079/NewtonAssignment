using AutoMapper;
using DLCGGameCore.Application.DTOs;
using DLCGGameCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DLCGGameCore.Application.Mappings
{
    public class VideoGameProfile: Profile
    {
        public VideoGameProfile()
        {
            CreateMap<VideoGame, VideoGameDto>().ReverseMap();
        }
    }
}
