# Huffman Encoding - Efficient Encoding Algorithm

Bu proje, C# ile Huffman Encoding algoritmasının detaylı ve elle yazılmış bir uygulamasıdır. Metin içindeki karakterlerin frekanslarına göre Huffman ağacı oluşturur, karakterlere özgü kodlar üretir ve metni sıkıştırır.


## İçindekiler

- [Proje Hakkında](#proje-hakkında)
- [Özellikler](#özellikler)
- [Örnek Çıktı](#örnek-çıktı)
- [Teknolojiler](#teknolojiler)
- [Katkıda Bulunma](#katkıda-bulunma)
- [Lisans](#lisans)
- [İletişim](#iletişim)


## Proje Hakkında

Huffman Encoding, veri sıkıştırmada kullanılan kayıpsız bir algoritmadır. Bu proje, verilen bir metindeki karakterlerin frekanslarını hesaplar, Huffman ağacı oluşturur ve karakterlere özgü değişken uzunlukta bit kodları üretir. Sonuç olarak metin, daha az bit kullanılarak sıkıştırılır.


## Özellikler

- Metindeki karakterlerin frekanslarını hesaplama
- Huffman ağacı oluşturma ve karakter kodlarını üretme
- Metni Huffman kodlarıyla sıkıştırma
- Konsola detaylı çıktı ve işlem süreleri yazdırma


Örnek Çıktı
Girilen metin: babadedefaca

Karakter	Frekans
b		   2
a		   4
d		   2
e		   2
f		   1
c		   1
------------------------
Frekansları Küçükten Büyüğe Sıralamada Geçen Süre: 0.0001 saniye
------------------------

Birleştirme sonucu oluşan dizgi: ...

-*-*-*-*-*-*-*-*-
Ağacın Sol Elemanları:
...

-*-*-*-*-*-*-*-*-
Ağacın Sağ Elemanları:
...

-*-*-*-*-*-*-*-*-
Ağacın Sol Kısmının Kodları:
...

-*-*-*-*-*-*-*-*-
Ağacın Sağ Kısmının Kodları:
...

Sıkıştırma öncesinde babadedefaca dizgisi hafızada 104 bit yer kaplamaktadır.

Huffman sıkıştırması sonucu oluşan dizgi:
101010011001...

Hafızada kapladığı yer: 68 bit
Sıkıştırma sonucu %34 oranında kâr edilmiştir.
Teknolojiler
C#
.NET 6.0
Katkıda Bulunma
Katkılarınızı memnuniyetle karşılarız! Lütfen bir sorun açın veya geliştirme önerilerinizi içeren pull request gönderin.

Lisans
Bu proje MIT Lisansı ile lisanslanmıştır. Detaylar için LICENSE dosyasına bakınız.

İletişim
Levent Demirkaya - GitHub - leventdemirkaya@outlook.com
