using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalisanMaasHesaplama
{
    class Program
    {
        struct calisanBilgileri {
            public long tcNo;
            public string ad, soyad;
            public double brutUcret, toplamBrut, gelirVergisi, engelliIndirimi, muafiyet, netUcret;
            public bool medeniDurum, calisanEs, engelliDurum;
            public int cocuk, buyukCocuk, kucukCocuk, engelliOran, esOdenegi, cocukOdenegi;

        }
        static void Main(string[] args)
        {
            Console.Write("Kaç çalışan için hesaplama yapılacak: ");
            int calisanSayisi = Convert.ToInt32(Console.ReadLine());
            int sayac = 0, secenek, gelirVergisi15 = 0, gelirVergisi20 = 0, gelirVergisi27 = 0, gelirVergisi35 = 0;

            calisanBilgileri[] calisan = new calisanBilgileri[calisanSayisi];
            do
            {
                //Çalışanın tc, ad, soyad ve brut ücretleri alıyoruz.
                Console.Write("Çalışanın TC Kimlik Numarasını Giriniz: ");
                calisan[sayac].tcNo = Convert.ToInt64(Console.ReadLine());
                Console.Write("Çalışanın Adı: ");
                calisan[sayac].ad = Console.ReadLine();
                Console.Write("Çalışanın Soyadı: ");
                calisan[sayac].soyad = Console.ReadLine();
                Console.Write("Çalışanın Aylık Brüt Ücreti: ");
                calisan[sayac].brutUcret = Convert.ToSingle(Console.ReadLine());
                
                //Çalışanın medeni durmunu ve evliyse eşinin çalışma durumunu alıyoruz.
                Console.Write("1.Evli\n");
                Console.Write("2.Bekar\n");
                do
                {
                    Console.Write("Çalışanın Medeni Durumu: ");
                    secenek = Convert.ToInt32(Console.ReadLine());
                } while (secenek < 1 || secenek > 2);
                if (secenek == 1) 
                    calisan[sayac].medeniDurum = true;
                else
                    calisan[sayac].medeniDurum = false;

                if (calisan[sayac].medeniDurum == true)
                {

                    Console.Write("1.Çalışıyor\n");
                    Console.Write("2.Çalışmıyor\n");

                    do
                    {
                        Console.Write("Çalışanın Eşinın Çalışma Durumu:");
                        secenek = Convert.ToInt32(Console.ReadLine());
                    } while (secenek < 1 || secenek > 2);

                    if (secenek == 1)
                        calisan[sayac].calisanEs = true;
                    else
                        calisan[sayac].calisanEs = false;
                }
                else
                    calisan[sayac].calisanEs = false;
                
                //Çalışanın bakmakla çocuk sayılarını buluyoruz.
                Console.Write("Çalışanın Bakmakla Yükümlü Çocuk Sayısı: ");
                calisan[sayac].cocuk = Convert.ToInt32(Console.ReadLine());
                if (calisan[sayac].cocuk > 0)
                {
                    do
                    {
                        Console.Write("6 Yaşından Büyük Çocuk Sayısı: ");
                        calisan[sayac].buyukCocuk = Convert.ToInt32(Console.ReadLine());
                        calisan[sayac].kucukCocuk = calisan[sayac].cocuk - calisan[sayac].buyukCocuk;
                    } while (calisan[sayac].buyukCocuk > calisan[sayac].cocuk);
                }

                //Çalışanın engellilik durumunu alıyoruz.
                do
                {
                    secenek = 0;
                    Console.Write("1.Engelli\n");
                    Console.Write("2.Engelli Değil\n");
                    Console.Write("Çalışanın Engellilik Durumu: ");
                    secenek = Convert.ToInt32(Console.ReadLine());
                } while (secenek < 1 || secenek > 2);

                if (secenek == 1)
                    calisan[sayac].engelliDurum = true;
                else
                    calisan[sayac].engelliDurum = false;

                if (calisan[sayac].engelliDurum == true)
                {
                    do
                    {
                        Console.Write("Çalışanın Engellilik Oranı: ");
                        calisan[sayac].engelliOran = Convert.ToInt32(Console.ReadLine());
                    } while (calisan[sayac].engelliOran < 1 || calisan[sayac].engelliOran > 100);
                }
                else
                    calisan[sayac].engelliOran = 0;

                sayac++;
            } while (sayac < calisanSayisi);

            //Ekrana Yazdırma İşlemleri
            secenek = 0;
            Console.WriteLine();
            for (int i = 0; i < sayac; i++)
            {
                //Tüm çalışanları ekrana yazdırıyoruz
                secenek++;
                Console.WriteLine("\n***"+ secenek + ". Çalışanın Bilgileri ***");
                Console.WriteLine("TC: "+ calisan[i].tcNo);
                Console.WriteLine("Ad: "+ calisan[i].ad);
                Console.WriteLine("Soyad: "+ calisan[i].soyad);
                Console.WriteLine("Brüt Ücret: "+ calisan[i].brutUcret);
                if (calisan[i].calisanEs == true)
                    calisan[i].esOdenegi = 0;
                else
                    calisan[i].esOdenegi = 220;
                if (calisan[i].medeniDurum == false)//Evli olmayanlarında eşinin çalışma durumu false olduğu için ek ödeneği sıfırlıyoruz.
                    calisan[i].esOdenegi = 0;
                Console.WriteLine("Eş Ödeneği: "+ calisan[i].esOdenegi);
                if (calisan[i].cocuk > 0)
                    calisan[i].cocukOdenegi = (calisan[i].kucukCocuk * 25) + (calisan[i].buyukCocuk * 45);
                else
                    calisan[i].cocukOdenegi = 0;
                Console.WriteLine("Çocuk İçin Aile Ödeneği: "+ calisan[i].cocukOdenegi);
                calisan[i].toplamBrut = calisan[i].brutUcret + calisan[i].esOdenegi + calisan[i].cocukOdenegi;
                Console.WriteLine("Aylık Toplam Brüt Ücret: " + calisan[i].toplamBrut);

                //Vergi kesinti hesaplıyoruz ve bu kesintiden kaç kişinin etkilendiğini görmek için sayaç koydum
                if (calisan[i].toplamBrut <= 2000)
                {
                    calisan[i].gelirVergisi = calisan[i].toplamBrut * 0.15;
                    Console.WriteLine("Aylık Gelir Vergisi Kesintisi: " + calisan[i].gelirVergisi);
                    gelirVergisi15++;
                }
                if (calisan[i].toplamBrut > 2000 && calisan[i].toplamBrut <= 5000)
                {
                    calisan[i].gelirVergisi = calisan[i].toplamBrut * 0.20;
                    Console.WriteLine("Aylık Gelir Vergisi Kesintisi: " + calisan[i].gelirVergisi);
                    gelirVergisi20++;
                }
                if (calisan[i].toplamBrut > 5000 && calisan[i].toplamBrut <= 10000)
                {
                    calisan[i].gelirVergisi = calisan[i].toplamBrut * 0.27;
                    Console.WriteLine("Aylık Gelir Vergisi Kesintisi: " + calisan[i].gelirVergisi);
                    gelirVergisi27++;
                }
                if (calisan[i].toplamBrut > 10000)
                {
                    calisan[i].gelirVergisi = calisan[i].toplamBrut * 0.35;
                    Console.WriteLine("Aylık Gelir Vergisi Kesintisi: " + calisan[i].gelirVergisi);
                    gelirVergisi35++;
                }
                
                //Engelli olan çalışanların engellilik indirimlerini hesaplıyoruz ve ekrana yazdırıyoruz.
                if(calisan[i].engelliOran >= 80)
                {
                    calisan[i].engelliIndirimi = 900;
                    Console.WriteLine("Engelli Vergi Muhafiyeti: " + calisan[i].engelliIndirimi);
                }
                if (calisan[i].engelliOran >= 60 && calisan[i].engelliOran < 80)
                {
                    calisan[i].engelliIndirimi = 470;
                    Console.WriteLine("Engelli Vergi Muhafiyeti: " + calisan[i].engelliIndirimi);
                }
                if (calisan[i].engelliOran >= 40 && calisan[i].engelliOran < 60)
                {
                    calisan[i].engelliIndirimi = 210;
                    Console.WriteLine("Engelli Vergi Muhafiyeti: " + calisan[i].engelliIndirimi);
                }
                if (calisan[i].engelliOran <40)
                {
                    calisan[i].engelliIndirimi = 0;
                    Console.WriteLine("Engelli Vergi Muhafiyeti: " + calisan[i].engelliIndirimi);
                }

                //Bu kısımda engellilik indirimi gelri vergisinden büyükse gelir vergisi möuafiyetini sıfırlıyoruz ki eksili sonuç çıkmasın.
                if (calisan[i].engelliIndirimi > calisan[i].gelirVergisi)
                    calisan[i].muafiyet = 0;
                else
                    calisan[i].muafiyet = calisan[i].gelirVergisi - calisan[i].engelliIndirimi;

                calisan[i].netUcret = calisan[i].toplamBrut - calisan[i].muafiyet;
                Console.WriteLine("Aylık Net Ücret: " + calisan[i].netUcret);
            }
            //
            double aylikToplamNet = 0, aylikGelir = 0, brutOrt = 0, netOrt = 0, temp = 0, temp2= 0;
            int sayacNet = 0, sayacBrut = 0, sayacNet2 = 0, sayacEvli = 0, sayacBekar = 0, sayacEs= 0;

            //Bu kısımda tüm çalışanlara ödenen aylık net ücret, devlete ödenen toplam vergi, aylık brüt ve net ücret ortalamaları, evli ve bekar olanları
            //çalışlan eş sayısını hesaplıyoruz
            foreach(calisanBilgileri c in calisan)
            {
                aylikToplamNet = aylikToplamNet + c.netUcret;
                aylikGelir = aylikGelir + c.gelirVergisi;
                brutOrt = brutOrt + c.toplamBrut;
                netOrt = netOrt + c.netUcret;
                if (c.netUcret < 2000)
                    sayacNet++;
                if (c.medeniDurum == true)
                    sayacEvli++;
                else
                    sayacBekar++;
                if (c.calisanEs == true)
                    sayacEs++;
            }
            
            //Brüt ücret ve net ücreti en yüksek olan çalışanı buluyoruz ve onun sırasını sayaçlara kaydediyoruz.
            //Genel bilgiler kısmında kaydettiğimiz sıradaki çalışanı yazdırıyoruz.
            for(int i = 0; i<calisanSayisi; i++)
            {
                if (temp < calisan[i].toplamBrut)
                {
                    temp = calisan[i].toplamBrut;
                    sayacBrut = i;
                }
                if (temp2 < calisan[i].netUcret)
                {
                    temp2 = calisan[i].netUcret;
                    sayacNet2 = i;
                }
            }
            //Genel bilgileri yazdırıyoruz
            Console.WriteLine("\n**** GENEL BİLGİLER****");
            Console.WriteLine("Tüm Çalışanlara Ödenen Toplam Net Ücret: " + aylikToplamNet);
            Console.WriteLine("Aylık Devlete Aktarılan Vergi: " + aylikGelir);
            brutOrt = brutOrt / calisanSayisi;
            Console.WriteLine("Tüm Çalışanların Aylık Brüt Ücretlerinin Ortalaması: " + brutOrt);
            netOrt = netOrt / calisanSayisi;
            Console.WriteLine("Tüm Çalışanların Aylık Net Ücretlerinin Ortalaması: " + netOrt);
            Console.WriteLine("2000TL'nin Altında Net Ücret Alan Çalışan Sayısı: " + sayacNet);
            Console.WriteLine("Gelir Vergisi %15 Olan Kişi Sayısı: " + gelirVergisi15);
            Console.WriteLine("Gelir Vergisi %20 Olan Kişi Sayısı: " + gelirVergisi20);
            Console.WriteLine("Gelir Vergisi %27 Olan Kişi Sayısı: " + gelirVergisi27);
            Console.WriteLine("Gelir Vergisi %35 Olan Kişi Sayısı: " + gelirVergisi35);
            Console.WriteLine("Brüt Ücreti En Yüksek Olan Kişi: \n" + calisan[sayacBrut].tcNo + "\n"+
               calisan[sayacBrut].ad + "\n" + calisan[sayacBrut].soyad + "\n" + calisan[sayacBrut].toplamBrut + "\n" +
               calisan[sayacBrut].gelirVergisi + "\n" + calisan[sayacBrut].netUcret);
            Console.WriteLine("Net Ücreti En Yüksek Olan Kişi: \n" + calisan[sayacNet2].tcNo + "\n" +
               calisan[sayacNet2].ad + "\n" + calisan[sayacNet2].soyad + "\n" + calisan[sayacNet2].toplamBrut + "\n" +
               calisan[sayacNet2].gelirVergisi + "\n" + calisan[sayacNet2].netUcret);
            Console.WriteLine("Evli Olan Çalışan Sayısı: " + sayacEvli);
            Console.WriteLine("Bekar Olan Çalışan Sayısı: " + sayacBekar);
            Console.WriteLine("Çalışan Eşi Çalışma Sayısı:  " + sayacEs);




            Console.ReadKey();
        }
    }
}
