using System;

using System.Collections.Generic; // Dizginin sınırı bilinmediği için liste içinde frekans - karakter bulunması yapılacaktır.
using System.Diagnostics; // Stopwatch kullanarak metodların hesaplama süresi kontrol edilip optimize koda ulaşmaya çalışılacaktır.

namespace EfficientEncodingAlgorithm
{
    class Program
    {
        static List<ushort> frekanslar = new List<ushort>(); // frekanslar listesinde max. 65,535 kez tekrar edebilen bir karaktere sahip dizginin frekansları tutulacaktır.
        static List<char> karakterler = new List<char>(); // string veri tipinde alınan dizgi, char veri tipine indirgenerek karakter-frekans'ın aynı indis ile kontrolü sağlanacaktır. 
        static string metin = "babadedefaca"; // sıkıştırılma işlemi uygulanacak metin
        static List<string> dizi = new List<string>(); //huffmanEncoding metodundan return edilen listenin elemanları, dizinin elemanı olacak.
        static List<string> solTaraf = new List<string>(), sagTaraf = new List<string>();
        static List<string> solKod = new List<string>(), sagKod = new List<string>();
        static List<string> HuffmanCoding = new List<string>();
        static string dizgiKod = ""; //metinin sıkıştırılması sonucu her bir harfin bit halinde ifade edilip sıkıştırılması
        /* Bütün listeler main sınıf haricinde de erişime açık olabilmesi için static olarak tanımlanmıştır.*/
        static void Main(string[] args)
        {
            Console.WriteLine($"Girilen metin: {metin}");
            Stopwatch sw = new Stopwatch();
            sw = frekansBul(); // metindeki karakterler listelere dizgideki sıralarına göre eklenmektedir. 
            karakterFrekansCiktisi(sw, "Frekansları Küçükten Büyüğe Sıralamada"); // huffmanAğacı oluşturulması için frekanslar küçükten büyüğe doğru sıralandı.
            dizi = huffmanEncoding(); // huffmanEncoding() metodunun return ettiği liste ile ağaçta decoding işlemi yapılıp bit değerleri oluşturulacak.
            short length = (short)(dizi.Count - 1); // Her bir harfin kodunun bulunması için ağaç üstten yapraklara kadar ayrıştırılacaktır.
            byte flag = 0; // sol veya sağ listeye ekleme yapılacağının belirlenmesi için kullanılacaktır.
            for (short i = length; i > 0; i--)
            {
                short j = (short)(i - 1);
                short indis = (short)(dizi[i].IndexOf(dizi[j])); // i dizgi, j.dizgiyi içermiyorsa indis -1 değerine sahip olacaktır. 
                if (indis < 0) // i.dizgisinin içinde j dizgisi yoksa
                {
                    while (indis < 0) // while döngüsü ile >=0 olana kadar veya liste sonuna kadar arama yapılacak
                    {
                        if (--j < 0) break;
                        indis = (short)(dizi[i].IndexOf(dizi[j]));
                    }
                    if (indis < 0) // Listede uyuşan eleman bulunamadığı için harften oluşmaktadır ve yapraklara ayrılacaktır.
                    {
                        if (solTaraf.Contains(dizi[i])) flag = 0; // i.dizgi solTarafta bulunuyorsa solTarafa ekleme yapılacaktır.
                        else flag = 1;
                        tekliEkle(flag, i);
                    }
                    else if (indis == 0) // j.dizgi i.dizginin 0.index'inden başladığı için j.dizgi i.dizginin solTarafında olacaktır.
                    {
                        if (solTaraf.Contains(dizi[i])) flag = 0; // eğer solTaraf'ta ise flag 0 yapılıp solTarafa ekleme yapılması sağlanacaktır.
                        else flag = 1;
                        solaEkle(flag, i, j);
                    }
                    else // j.dizgi >0 gibi bir index değerine sahip olduğu için i.dizginin sağTarafında olacaktır.
                    {
                        if (sagTaraf.Contains(dizi[i])) flag = 1; // sağTarafta ise flag 1 yapılıp sağTarafa ekleme yapılacaktır.
                        else flag = 0;
                        sagaEkle(flag, i, j);
                    }
                    j = (short)(i - 1); // j değerini ilk haline getirdik.
                }
                else if (indis == 0) // j.dizgi i.dizginin 0.index'inden başladığı için j.dizgi i.dizginin solTarafında olacaktır.
                {
                    flag = 0; // flag 0 yapılıp solTaraf'a ekleme yapılması sağlanmıştır.
                    solaEkle(flag, i, j);
                }
                else // j.dizgi, 0'dan büyük bir index değerine sahip olduğu için i.dizginin sağTarafında olacaktır.
                {
                    flag = 1; // sagTarafa ekleme yapılması için flag=1 değeri atandı.
                    sagaEkle(flag, i, j);
                }
                if ((i - 1) == 0) // for döngüsündeki koşuldan ötürü i değerinin min.değeri 1 olacaktır ve son eleman yapraklarına ayrılamadığı için en son ayırma işlemi burada yapılacaktır.
                {
                    if (solTaraf.Contains(dizi[i - 1])) flag = 0; // flag 0 atandı ve solTarafa ekleme yapılacaktır.
                    else flag = 1;
                    tekliEkle(flag, (short)(i - 1));
                }
            }
            #region Ağaç Ekran_Çıktısı
            flag = 0;
            Console.WriteLine("-*-*-*-*-*-*-*-*-\nAğacın Sol Elemanları:");
            while (flag < solTaraf.Count)
                Console.WriteLine(solTaraf[flag++]);
            flag = 0;
            Console.WriteLine("-*-*-*-*-*-*-*-*-\nAğacın Sağ Elemanları:");
            while (flag < sagTaraf.Count)
                Console.WriteLine(sagTaraf[flag++]);
            Console.WriteLine("-*-*-*-*-*-*-*-*-\nAğacın Sol Kısmının Kodları:");
            solKodOlustur();
            Console.WriteLine("-*-*-*-*-*-*-*-*-\nAğacın Sağ Kısmının Kodları:");
            sagKodOlustur();
            dizi.Clear(); frekanslar.Clear();
            #endregion
            Console.WriteLine($"Sıkıştırma öncesinde {metin} dizgisi hafızada {metin.Length * 8} bit yer kaplamaktadır.\n");
            DizgiyiKodla(" ");
            Console.ReadKey();
        }
        static Stopwatch frekansBul()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (ushort i = 0; i < metin.Length; i++)
                if (!karakterler.Contains(metin[i])) // eğer karakterler listesinde dizginin i.indisteki karakteri yoksa karşılaştırma yap. [Backtracking]
                {
                    ushort bulunan = 1; // dizgideki her karakter en az 1 kez tekrar etmektedir.
                    for (ushort j = (ushort)(i + 1); j < metin.Length; j++) if (metin[j] == metin[i]) bulunan++;
                    karakterler.Add(metin[i]);
                    frekanslar.Add(bulunan);
                }
            karakterFrekansCiktisi(sw, "Frekans Tablosu Oluşturmada");
            sw = new Stopwatch(); sw.Start(); // stopwatch yenilendi. 0'dan tekrar başlayacak sort için.
            /* Bubble Sort ile frekansları küçükten büyüğe sıralama */
            for (ushort sayac = 0; sayac < frekanslar.Count; sayac++)
                for (ushort i = 0; i < frekanslar.Count - 1; i++)
                {
                    ushort j = (ushort)(i + 1);
                    if (frekanslar[i] > frekanslar[j])
                    {
                        ushort tempFrekans = frekanslar[j];
                        char tempKarakter = karakterler[j];
                        frekanslar[j] = frekanslar[i];
                        karakterler[j] = karakterler[i];
                        frekanslar[i] = tempFrekans;
                        karakterler[i] = tempKarakter;
                    }
                }
            return sw;
        }
        static List<string> huffmanEncoding()
        {
            List<string> huffmanListesi = new List<string>(), tempHuf = new List<string>(); // HuffmanListesinde karakterler char listesindeki karakterlerin birleştirilmiş hali son 1 eleman kalıncaya dek tutuluyor, tempHuf'ta Aşama aşama birleştirilen dizgiler tutuluyor.
            List<ushort> tempFrekanslar = new List<ushort>(); // frekanslar listesini kaybetmemek için yedekleme üzerinden ekleme işlemi yapılıyor.
            for (ushort i = 0; i < karakterler.Count; i++)
            {
                huffmanListesi.Add(karakterler[i].ToString()); // karakterler listesi char veri tipindedir ve += operatörüyle işlem yapılamadığı için string listesine karakterleri taşıdık.
                tempFrekanslar.Add(frekanslar[i]); // tempFrekanslar = frekanslar şeklinde liste eşitlemesi yapılsaydı pointer yapısından ötürü frekanslar listesinde de değişiklik olacaktı.
            }
            ushort islemSayisi = (ushort)(karakterler.Count - 1); // Birleştirme işlemi, ilgili dizgide length'ten 1 eksik olana dek devam eder
            for (ushort i = 0; islemSayisi > 0; islemSayisi--, i = 0) // Huffman birleştirmesinde sadece 0.indisle işlem yapılıyor ve işlemSayısını da her tur güncellememiz gerekli.
            {
                ushort tempFrekans = (ushort)(tempFrekanslar[i] + tempFrekanslar[i + 1]); //frekansları toplama işlemi 
                string tempKarakter = huffmanListesi[i] + huffmanListesi[i + 1]; // karakterleri sıkıştırma işlemi
                tempHuf.Add(tempKarakter);
                for (ushort j = 0; j < 2; j++) // birleştirilmeden önceki hallerini listelerden kaldırıyoruz.
                {
                    huffmanListesi.RemoveAt(i);
                    tempFrekanslar.RemoveAt(i);
                }
                while (islemSayisi > 1 && tempFrekans >= tempFrekanslar[i]) // Birleştirilmiş haldeki karakterin hangi indekse insert edileceği bulunuyor.
                { // eğer işlemSayısı 1 ise yani son adımda ise birleştirme işlemi tamamlanmıştır ve sadece count'ı 1 olan bir string değişken listede bulunmaktadır.
                    i++;
                    if (i >= tempFrekanslar.Count) //eğer i, listedeki frekansların adedinden büyükse direkt listenin en son indeksine ekleme yapılıyor.
                    {
                        i = (ushort)(tempFrekanslar.Count);
                        break;
                    }
                }
                huffmanListesi.Insert(i, tempKarakter);
                tempFrekanslar.Insert(i, tempFrekans);
                tempKarakter = "";
            }
            Console.WriteLine($"\nBirleştirme sonucu oluşan dizgi: {tempHuf[tempHuf.Count - 1]}\n");
            return tempHuf;
        }
        static void karakterFrekansCiktisi(Stopwatch sw, String mesaj)
        {
            Console.WriteLine("\nKarakter\tFrekans");
            for (ushort i = 0; i < frekanslar.Count; i++) Console.WriteLine($"{karakterler[i]}\t\t   {frekanslar[i]}");
            Console.WriteLine("------------------------");
            if (mesaj != null)
            {
                Console.WriteLine($"{mesaj} Geçen Süre: {sw.Elapsed.TotalSeconds} saniye");
                for (int i = 0; i < 68; i++)
                    Console.Write("_");
                Console.WriteLine();
            }
        }
        static void solaEkle(byte yon, short i, short j)
        {
            if (i == dizi.Count - 1) // İlk turda sol ve sağ kısımlara ayırma işlemi yapılmaktadır.
            {
                solTaraf.Add(dizi[j]);
                sagTaraf.Add(dizi[i].Substring(dizi[j].Length, dizi[i].Length - dizi[j].Length));
            }
            else
            {
                if (yon % 2 == 0) // sol tarafa ekleme yapılacaktır.
                {
                    if (solTaraf[0].Length != 1) // 1 uzunluğa sahip kelimelere ekleme yapılmamaktadır, otomatik olarak yaprak durumuna geçmiştir.
                    {
                        solTaraf.Add(dizi[j]); //solTarafın ilk elemanı j'nin kendisi, diğer eleman da j.nin uzunluğundan sonra i ile j'nin farkı uzunluğuna sahip eleman olacaktır.
                        solTaraf.Add(dizi[i].Substring(dizi[j].Length, dizi[i].Length - dizi[j].Length));
                    }
                    else
                    {
                        sagTaraf.Add(dizi[j]);
                        sagTaraf.Add(dizi[i].Substring(dizi[j].Length, dizi[i].Length - dizi[j].Length));
                    }
                }
                else
                {
                    if (sagTaraf[0].Length != 1) // 1 uzunluğa sahip kelimelere ekleme yapılmamaktadır, otomatik olarak yaprak durumuna geçmiştir.
                    {
                        sagTaraf.Add(dizi[j]);
                        sagTaraf.Add(dizi[i].Substring(dizi[j].Length, dizi[i].Length - dizi[j].Length));
                    }
                }
            }
        }
        static void sagaEkle(byte yon, short i, short j)
        {
            if (i == dizi.Count - 1) // İlk turda sağ ve sol kısımlara ayırma yapmaktadır.
            {
                sagTaraf.Add(dizi[j]);
                solTaraf.Add(dizi[i].Substring(0, dizi[i].Length - dizi[j].Length));
            }
            else
            {
                if (yon % 2 == 0) //solTarafaEkle
                {
                    if (solTaraf[0].Length != 1)  // 1 uzunluğa sahip kelimelere ekleme yapılmamaktadır, otomatik olarak yaprak durumuna geçmiştir.
                    {
                        solTaraf.Add(dizi[i].Substring(0, dizi[i].Length - dizi[j].Length));
                        solTaraf.Add(dizi[j]);
                    }
                    else
                    {
                        sagTaraf.Add(dizi[j]);
                        sagTaraf.Add(dizi[i].Substring(dizi[j].Length, dizi[i].Length - dizi[j].Length));
                    }
                }
                else // sağTarafaEkle
                {
                    if (sagTaraf[0].Length != 1)  // Uzunluğu 1 olan kelimelere ekleme yapılamamaktadır, otomatik olarak yaprak durumuna geçmiştir.
                    {
                        sagTaraf.Add(dizi[i].Substring(0, dizi[i].Length - dizi[j].Length));
                        sagTaraf.Add(dizi[j]);
                    }
                    else
                    {
                        solTaraf.Add(dizi[j]); //solTarafın ilk elemanı j'nin kendisi, diğer eleman da j.nin uzunluğundan sonra i ile j'nin farkı uzunluğuna sahip eleman olacaktır.
                        solTaraf.Add(dizi[i].Substring(dizi[j].Length, dizi[i].Length - dizi[j].Length));
                    }
                }
            }
        }
        static void tekliEkle(byte yon, short i)
        {
            if (yon % 2 == 0) // solTarafa ekleyecek
            {
                solTaraf.Add(dizi[i].Substring(0, 1));
                solTaraf.Add(dizi[i].Substring(1, 1));
            }
            else //sağTarafa ekleyecek
            {
                sagTaraf.Add(dizi[i].Substring(0, 1));
                sagTaraf.Add(dizi[i].Substring(1, 1));
            }
        }
        static void solKodOlustur()
        {
            ushort[] indisDizisi = new ushort[solTaraf.Count];
            string[] eklenecekKod = new string[solTaraf.Count];
            ushort bulunan = 0, yerelSayac = 0, i = 1;
            solKod.Add("0");
            while (bulunan < solTaraf[0].Length)
            {
                if (solTaraf[i - 1].Length > 1)
                {
                    if (solTaraf[i - 1][0] == solTaraf[i][0]) // eğer solTaraf[i-1].elemanı, solTaraf[i].elemanının parent düğümü ise
                    {
                        if ((i - 1) >= solKod.Count) // solKod'un length'inden büyük olduğu için ekleme işlemi için ek şartlar gereklidir.
                            for (ushort temp1 = 0; temp1 < yerelSayac; temp1++)
                                if (eklenecekKod[temp1] != null) // eklenecekKod listesinde eleman varsa
                                {
                                    if (indisDizisi[temp1] < solTaraf.Count - 2) // solTaraf listesinin son iki elemanına kadar listeye ekleme yapılır.
                                    {
                                        solKod.Add(eklenecekKod[temp1]);
                                        eklenecekKod[temp1] = null; // ekledikten sonra ilgili indise null ve 0 değerlerini atanır.
                                        indisDizisi[temp1] = 0;
                                    }
                                    else
                                    {
                                        if (eklenecekKod[0] == null) // son iki elemanın listedeki yerleri güncellenir
                                        {
                                            eklenecekKod[0] = eklenecekKod[temp1];
                                            eklenecekKod[temp1] = null;
                                            indisDizisi[0] = indisDizisi[temp1];
                                            indisDizisi[temp1++] = 0;
                                            eklenecekKod[1] = eklenecekKod[temp1];
                                            eklenecekKod[temp1] = null;
                                            indisDizisi[1] = indisDizisi[temp1];
                                            indisDizisi[temp1] = 0;
                                        }
                                    }
                                }
                        solKod.Add(solKod[i - 1] + "0");
                        solKod.Add(solKod[i - 1] + "1");
                    }
                    else // solTaraf[i-1].elemanının child düğümünü arıyoruz.
                    {
                        ushort j = i;
                        while (solTaraf[i - 1][0] != solTaraf[j][0]) // [i-1].elemanın birinci karakteriyle uyuşan elemanı bulana dek j++
                        {
                            if (j > solTaraf.Count - 1) break;
                            j++;
                        }
                        if ((i - 1) < solKod.Count) // [i-1].eleman solKod'un length'inden küçükse
                        {
                            indisDizisi[yerelSayac] = j; //indisDizisine j.indisi ekledik
                            eklenecekKod[yerelSayac++] = solKod[i - 1] + "0"; //eklenecek kod dizisine parent node'un sol tarafında olduğu için parent node'a 0 ekleyip diziye ekledik. 
                            indisDizisi[yerelSayac] = ++j; // indisDizisinin yerelSayac değerini güncelledikten sonra j'den sonraki elemanı da ekledik.
                            eklenecekKod[yerelSayac++] = solKod[i - 1] + "1"; // parent node'un sağ tarafında olduğu için 1 ekleyip eklenecekKod dizisine ekledik.
                        }
                        else // eğer i, solKod'un length'inden büyük veya eşit ise
                        {
                            for (ushort k = 0; k < eklenecekKod.Length; k++)
                            {
                                if (solTaraf[i - 1] == solTaraf[indisDizisi[k]]) // [i-1].eleman, solTaraf listesinde indisDizisi[k]'ya denk gelen eleman ise
                                {
                                    indisDizisi[yerelSayac] = j;
                                    eklenecekKod[yerelSayac++] = eklenecekKod[k] + "0"; // parent node eklenecekKod dizisindedir ve bu dizi de sol tarafta old.için parentNode'a 0 ekleyerek elemanı ekledik.
                                    indisDizisi[yerelSayac] = ++j;
                                    eklenecekKod[yerelSayac++] = eklenecekKod[k] + "1"; // sagTarafta olduğu için parentNode'a 1 ekleyerek elemanı eklenecekKod dizisine ekledik.
                                    break;
                                }
                            }
                        }
                    }
                }
                else bulunan++; // eğer [i-1].eleman 1 uzunluğa sahipse
                i++;
            }
            if (eklenecekKod[0] != null) // eğer eklenecek eleman varsa
            {
                BubbleSortKodEkleme(indisDizisi, eklenecekKod); // kodların doğru sırada eklenmesi için bubbleSort ile indisDizisindeki sıraya göre ekleme yapılmaktadır.
                for (ushort j = 0; j < eklenecekKod.Length; j++)
                {
                    if (eklenecekKod[j] == null) break; // null değerle karşılaşılırsa eklemeler tamamlanmıştır ve brake anahtar kelimesiyle döngüden çıkılır. [backtracking]
                    solKod.Add(eklenecekKod[j]);
                    eklenecekKod[j] = null;
                    indisDizisi[j] = 0;
                }
            }
            yerelSayac = 0;
            foreach (string j in solKod)
            {
                if (solTaraf[yerelSayac].Length == 1) Console.WriteLine($"{solTaraf[yerelSayac]} :\t{j}");
                else Console.WriteLine($"  :\t{j}");
                yerelSayac++;
            }
        }
        static void sagKodOlustur()
        {
            ushort[] indisDizisi = new ushort[sagTaraf.Count];
            string[] eklenecekKod = new string[sagTaraf.Count];
            ushort bulunan = 0, yerelSayac = 0, i = 1;
            sagKod.Add("1");
            while (bulunan < sagTaraf[0].Length)
            {
                if (sagTaraf[i - 1].Length > 1)
                {
                    if (sagTaraf[i - 1][0] == sagTaraf[i][0]) // eğer sağTaraf[i-1].elemanı, sağTaraf[i].elemanının parent düğümü ise
                    {
                        if ((i - 1) >= sagKod.Count) // sağKod'un length'inden büyük olduğu için ekleme işlemi için ek şartlar gereklidir.
                            for (ushort temp1 = 0; temp1 < yerelSayac; temp1++)
                                if (eklenecekKod[temp1] != null) // eklenecekKod listesinde eleman varsa
                                {
                                    if (indisDizisi[temp1] < sagTaraf.Count - 2) // sağTaraf listesinin son iki elemanına kadar listeye ekleme yapılır.
                                    {
                                        sagKod.Add(eklenecekKod[temp1]);
                                        eklenecekKod[temp1] = null; // ekledikten sonra ilgili indise null ve 0 değerlerini atanır.
                                        indisDizisi[temp1] = 0;
                                    }
                                    else
                                    {
                                        if (eklenecekKod[0] == null) // son iki elemanın listedeki yerleri güncellenir
                                        {
                                            eklenecekKod[0] = eklenecekKod[temp1];
                                            eklenecekKod[temp1] = null;
                                            indisDizisi[0] = indisDizisi[temp1];
                                            indisDizisi[temp1++] = 0;
                                            eklenecekKod[1] = eklenecekKod[temp1];
                                            eklenecekKod[temp1] = null;
                                            indisDizisi[1] = indisDizisi[temp1];
                                            indisDizisi[temp1] = 0;
                                        }
                                    }
                                }
                        sagKod.Add(sagKod[i - 1] + "0");
                        sagKod.Add(sagKod[i - 1] + "1");
                    }
                    else // sağTaraf[i-1].elemanının child düğümünü arıyoruz.
                    {
                        ushort j = i;
                        while (sagTaraf[i - 1][0] != sagTaraf[j][0]) // [i-1].elemanın birinci karakteriyle uyuşan elemanı bulana dek j++
                        {
                            if (j > sagTaraf.Count - 1) break;
                            j++;
                        }
                        if ((i - 1) < sagKod.Count) // [i-1].eleman sağKod'un length'inden küçükse
                        {
                            indisDizisi[yerelSayac] = j; //indisDizisine j.indisi ekledik
                            eklenecekKod[yerelSayac++] = sagKod[i - 1] + "0"; //eklenecek kod dizisine parent node'un sol tarafında olduğu için parent node'a 0 ekleyip diziye ekledik.
                            indisDizisi[yerelSayac] = ++j; // indisDizisinin yerelSayac değerini güncelledikten sonra j'den sonraki elemanı da ekledik.
                            eklenecekKod[yerelSayac++] = sagKod[i - 1] + "1"; // parent node'un sağ tarafında olduğu için 1 ekleyip eklenecekKod dizisine ekledik.
                        }
                        else // eğer i, sağKod'un length'inden büyük veya eşit ise
                        {
                            for (ushort k = 0; k < eklenecekKod.Length; k++)
                            {
                                if (sagTaraf[i - 1] == sagTaraf[indisDizisi[k]]) // [i-1].eleman, sagTaraf listesinde indisDizisi[k]'ya denk gelen eleman ise
                                {
                                    indisDizisi[yerelSayac] = j;
                                    eklenecekKod[yerelSayac++] = eklenecekKod[k] + "0"; // parent node eklenecekKod dizisindedir ve bu dizi de sol tarafta old.için parentNode'a 0 ekleyerek elemanı ekledik.
                                    indisDizisi[yerelSayac] = ++j;
                                    eklenecekKod[yerelSayac++] = eklenecekKod[k] + "1";  // sagTarafta olduğu için parentNode'a 1 ekleyerek elemanı eklenecekKod dizisine ekledik.
                                    break;
                                }
                            }
                        }
                    }
                }
                else bulunan++; // eğer [i-1].eleman 1 uzunluğa sahipse
                i++;
            }
            if (eklenecekKod[0] != null) // eğer eklenecek eleman varsa
            {
                BubbleSortKodEkleme(indisDizisi, eklenecekKod); // kodların doğru sırada eklenmesi için bubbleSort ile indisDizisindeki sıraya göre ekleme yapılmaktadır
                for (ushort j = 0; j < eklenecekKod.Length; j++)
                {
                    if (eklenecekKod[j] == null) break; // null değerle karşılaşılırsa eklemeler tamamlanmıştır ve brake anahtar kelimesiyle döngüden çıkılır. [backtracking]
                    sagKod.Add(eklenecekKod[j]);
                    eklenecekKod[j] = null;
                    indisDizisi[j] = 0;
                }
            }
            yerelSayac = 0;
            foreach (string j in sagKod)
            {
                if (sagTaraf[yerelSayac].Length == 1) Console.WriteLine($"{sagTaraf[yerelSayac]} :\t{j}");
                else Console.WriteLine($"  :\t{j}");
                yerelSayac++;
            }
            Console.WriteLine();
        }
        static void BubbleSortKodEkleme(ushort[] indisDizisi, string[] eklenecekKod)
        {
            ushort sayac; // 2'şer 2'şer işleme alındığı için while döngüsünde kullanılmak üzere oluşturulmuştur.
            for (ushort j = 0; j < indisDizisi.Length - 1; j = (ushort)(j + 2)) // indisler ve eklenecekKodlar ardışık olarak yerleştiği için güncelleme kısmında +2 şeklinde işleme alınmaktadır.
            {
                sayac = 2;
                ushort k = (ushort)(j + 2);
                if (indisDizisi[k] == 0) break;
                if (indisDizisi[j] > indisDizisi[k]) // indisi büyük olan kod önce eklenecekKod listesine eklenmişse while döngüsü yardımıyla doğru sırasına yerleştirilmektedir.
                {
                    while (sayac-- > 0)
                    {
                        ushort tempFrekans = indisDizisi[k];
                        string tempKarakter = eklenecekKod[k];
                        indisDizisi[k] = indisDizisi[j];
                        eklenecekKod[k++] = eklenecekKod[j];
                        indisDizisi[j] = tempFrekans;
                        eklenecekKod[j++] = tempKarakter;
                    }
                }
            }
        }
        static ushort DizgiyiKodla(string mesaj)
        {
            ushort hafizadaKapladıgiYer = 0;
            short indis;
            for (ushort i = 0; i < metin.Length; i++)
            {
                indis = (short)solTaraf.IndexOf(metin[i].ToString());
                if (indis != -1) // ilk harf, eğer sol tarafta bulunuyorsa
                {
                    dizgiKod += solKod[indis];
                    hafizadaKapladıgiYer += (ushort)solKod[indis].Length; // frekans*bit_uzunluğu ile eklenen her bir elemanın kod'unun length'ini toplamak aynı sonucu verir.
                    if (!dizi.Contains(metin[i].ToString()))
                    {
                        dizi.Add(metin[i].ToString());
                        frekanslar.Add((ushort)(solKod[indis].Length));
                        HuffmanCoding.Add(solKod[indis]);
                    }
                }
                else // harf sağ tarafta bulunuyorsa
                {
                    indis = (short)sagTaraf.IndexOf(metin[i].ToString());
                    dizgiKod += sagKod[indis];
                    hafizadaKapladıgiYer += (ushort)sagKod[indis].Length;
                    if (!dizi.Contains(metin[i].ToString())) 
                    {
                        dizi.Add(metin[i].ToString());
                        frekanslar.Add((ushort)(sagKod[indis].Length));
                        HuffmanCoding.Add(sagKod[indis]);
                    }
                }
            }
            if (mesaj != null) Console.WriteLine($"Huffman sıkıştırması sonucu oluşan dizgi:\n{dizgiKod}\nHafızada kapladığı yer: {hafizadaKapladıgiYer} bit\nSıkıştırma sonucu %{(metin.Length * 8 - hafizadaKapladıgiYer) * 100 / (metin.Length * 8)} oranında kâr edilmiştir.\n");
            return hafizadaKapladıgiYer;
        }
    }
}