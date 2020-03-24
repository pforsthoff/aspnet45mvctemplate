using System;
using System.Text;
using System.Web.Configuration;
using AutoMapper;
using DataDictionary.ServiceModel.Entities;


namespace DataDictionaryOverlay.Mappings
{
    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Metadata, Models.MetadataViewModel>()
                .ForMember(dest => dest.Actions,
                    opt => opt.MapFrom(src => MetadataTransforms.ProcessServerActionsAnchor(src.Id)));
        }
    }
}