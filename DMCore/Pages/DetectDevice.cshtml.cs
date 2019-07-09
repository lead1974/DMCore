using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Wangkanai.Detection;

namespace DMCore.Pages
{
    public class DetectDeviceModel : PageModel
    {
        private IDeviceResolver _device;

        public DetectDeviceModel(IDeviceResolver deviceResolver)
        {
            _device = deviceResolver;

        }
        public IActionResult OnGet()
        {
            if (_device.Device.Type == DeviceType.Desktop)
            {
                return RedirectToPage("/Default", new { area = "" });
            }
            else if (_device.Device.Type == DeviceType.Mobile)
            {
                return RedirectToPage("/Index", new { area = "Mobile" });
            }
            else if (_device.Device.Type == DeviceType.Tablet)
            {
                return RedirectToPage("/Index", new { area = "Mobile" });
            }
            return RedirectToPage("/Default", new { area = "" });
        }
    }
}