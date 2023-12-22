using Microsoft.AspNetCore.Mvc.RazorPages;
using webappsql.Certification;
using webappsql.Vendors;

namespace webappsql.Pages;

public class IndexModel : PageModel
{
    public List<Offerings>? course;

    //public void OnGet(VendorServices vendorServices)
    public void OnGet()
    {
       VendorServices vendorServices=new VendorServices();
        //VendorServices vendorServices1 = vendorServices;
       course = vendorServices.GetCourses();
    }
}