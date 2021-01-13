using NvPShop.ViewModel.System.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NvpShop.Application.System.Email
{
    public interface IServiceEmail
    {
        Task SendEmailAsync(Message message);
    }
}
