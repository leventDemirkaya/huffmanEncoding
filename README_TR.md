# 🌳 Huffman Encoding  

> ✨ **Açıklama:**  
> Bu proje, **C# .NET 6.0** ile Huffman Encoding algoritmasını **hafıza dostu** veri tipleri (listeler ve diziler) kullanarak uygular.  
> **Kayıpsız sıkıştırma** ile verileriniz daha az yer kaplar, daha hızlı taşınır.  

## 📑 İçindekiler  
- [📜 Proje Hakkında](#proje-hakkinda)  
- [⚡ Özellikler](#ozellikler)
- [🧩 Algoritma Özeti](#algoritma-özeti)  
- [⚙️ Kurulum ve Kullanım](#kurulum-ve-kullanim)  
- [📊 Örnek Çıktı](#ornek-cikti)  
- [🛠 Teknolojiler](#teknolojiler)  
- [🤝 Katkıda Bulunma](#katkida-bulunma)  
- [📄 Lisans](#lisans)  
- [📬 İletişim](#iletisim)

<a id="proje-hakkinda"></a>
## 📜 Proje Hakkında  
Huffman Encoding, verileri **karakter frekanslarına göre kodlayan** bir **kayıpsız sıkıştırma algoritmasıdır**.  

Bu proje:  
- 🔍 Karakter frekanslarını hesaplar  
- 🌲 Huffman ağacı mantığıyla birleşimleri oluşturur  
- 🔢 Karakter başına değişken uzunlukta bit kodları üretir  
- 📉 Sıkıştırma öncesi/sonrası bit karşılaştırması ve oranı raporlar  

<a id="ozellikler"></a>
## ⚡ Özellikler  
✅ Detaylı frekans analizi  
✅ Birleştirme adımlarını baz alarak kod üretimi  
✅ Bit seviyesinde sıkıştırma uzunluğu hesabı  
✅ Sıkıştırma öncesi / sonrası boyut karşılaştırması  
✅ Konsolda işlem süreleri ve kod listesi

<a id="algoritma-özeti"></a>
## 🧩 Algoritma Özeti
1. Girdi metindeki her karakterin frekansı hesaplanır.  
2. Karakterler frekanslarına göre küçükten büyüğe sıralanır.  
3. En düşük frekanslı iki karakter birleştirilerek Huffman ağacı oluşturulur.  
4. Ağaç yapraklarından köke doğru gidilerek her karaktere özgü bit dizisi atanır (0 = sol, 1 = sağ).  
5. Metin, bu bit dizileriyle kodlanarak sıkıştırılır.  
6. Sıkıştırma oranı ve işlem süresi hesaplanır.

<a id="kurulum-ve-kullanim"></a>
## ⚙️ Kurulum ve Kullanım  
1. 📥 Projeyi klonlayın:  
   ```bash
   git clone https://github.com/leventDemirkaya/huffmanEncoding.git
   cd huffmanEncoding

<a id="ornek-cikti"></a>

## 📊 Örnek Çıktı

### 📜 Girilen Metin
babadedefaca

## 🔢 Karakter Frekansları ve Huffman Kodları

| Karakter | Frekans | Huffman Kodu |
|----------|---------|--------------|
| a        | 4       | 0            |
| b        | 2       | 101          |
| d        | 2       | 100          |
| e        | 2       | 111          |
| f        | 1       | 1101         |
| c        | 1       | 1100         |

- ⏱ İşlem Süresi: 0.0001 saniye

- 📦 Sıkıştırmadan Önce: 104 bit

- 📦 Sıkıştırma Sonrası: 68 bit

- 📉 Sıkıştırma Oranı: %34 kazanç

### 📝 Sıkıştırılmış Veri
1010010101001111001111101011000

<a id="teknolojiler"></a>

## 🛠 Teknolojiler
- 💻 C#

- 🖥 .NET 6.0

- 📂 Konsol uygulaması (tek dosya ana akış: Program.cs)

<a id="katkida-bulunma"></a>

## 🤝 Katkıda Bulunma
💡 Katkılarınız çok değerlidir!

- 🐛 Hata bildirmek için Issues sekmesini kullanın

- 🚀 Geliştirme önerileri için Pull Request açın

<a id="lisans"></a>

## 📄 Lisans
📝 Bu proje MIT Lisansı ile korunmaktadır. Detaylar için LICENSE dosyasına bakabilirsiniz.

<a id="iletisim"></a>

## 📬 İletişim
📧 leventdemirkaya@outlook.com
