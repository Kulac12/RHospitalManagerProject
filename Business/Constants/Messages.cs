using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {

        //Polyclinic Mesajları
        public static string AddPolyclinic = "Poliklinik başarılı şekilde oluşturuldu";
        public static string DeletePolyclinic = "Poliklinik başarılı şekilde silindi";
        public static string UpdatePolyclinic = "Poliklinik başarılı şekilde güncellendi";

        public static string PolyclinicExists = "Poliklinik zaten var";
        public static string IsNullOrWhiteSpace = "Poliklinik adı boş olamaz.";
        public static string PolyclinicNotFound = "Poliklinik bulunamadı";
        

        //Appointment
        public static string AppointmentPoyclinicOrDoctorNotFound = "Poliklinik veya Doktor bilgisini bulunamadı.";
        public static string AppointmentAdded = "Randevu başarılı şekilde oluşturuldu.";
        public static string PolyclinikNotFound = "Poliklinik bulunamadı";
        public static string InvalidDateOrTime = "İlgili tarih ve saat dolu";
        public static string AppointmentAlreadyExists = "İlgili tarih ve saat dolu";



        //Doctor
        public static string DoctorNotFound = "Doktor bulunamadı";
        public static string PatientNotFound = "Hasta kaydı bulunamadı";






        //ProductManager ile ilgili bilgilendirme mesajları
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = " Ürün ismi geçersiz";
        public static string Bakimda = "Sistem şuan da bakımda, Anlayışınız için teşekkür ederiz";
        public static string ProductsListed = "Ürünler listelendi";
        public static string ProductListed = "Ürün listelendi";
        public static string ProductDetail = "Ürün detayları listelendi";


        //CategoryManager ile ilgili bilgilendirme mesajları
        public static string CategorysListed = "Kategoriler listelendi.";
        public static string CategoryListed = "İlgili kategori listelendi";

        public static string ProductCountOfCategoryError = "Kategoride 10 dan fazla ürün var";
        public static string SameProductNameTrue = "Aynı ürün isminde zaten bir ürün mevcut.";
        public static string CategoryLimitedExceded ="Kategori limiti aşıldı.";

        public static string? AuthorizationDenied = "Yetkiniz yok.";
        public static string UserRegistered = "Kullanıcı kayıt oldu.";

        public static string SuccessfulLogin = "Başarılı giriş.";
        public static string UserAlreadyExists = "UserAlreadyExists, böyle bir kullanıcı zaten mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";
        public static string UserNotFound = " Kullanıcı bulunamadı";

        public static string PasswordError = "Parola hatası";

      
    }
}

