using DonaMenina.Entities;
using DonaMenina.Models;

namespace DonaMenina.Helper
{
    public interface ISaleSession
    {
        void BuildSaleSession(WorkSpaceModel sale);
        void EndSaleSession();
        WorkSpaceModel GetSaleSession();
    }
}
