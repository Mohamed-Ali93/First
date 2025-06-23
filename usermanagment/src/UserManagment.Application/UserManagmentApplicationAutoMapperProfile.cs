using AutoMapper;
using Volo.Abp.Identity;
using UserManagment.SecurityLogs;

namespace UserManagment;

public class UserManagmentApplicationAutoMapperProfile : Profile
{
    public UserManagmentApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<IdentitySecurityLog, SecurityLogDto>();
    }
}
