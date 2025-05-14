using apiqxote.DTOModels;
using apiqxote.Models;
using AutoMapper;

namespace apiqxote.Profiles
{
    public class TreeNameProfile : Profile
    {
        public TreeNameProfile()
        {
            CreateMap<TreeName, TreeNameDTO>().ReverseMap();
        }
    }
}
