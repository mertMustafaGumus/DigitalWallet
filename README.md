# Digital Wallet Case Study Projesi

Bu proje, bir case study kapsamında geliştirilmiş modern bir dijital cüzdan uygulamasıdır. Clean Architecture prensiplerine uygun olarak geliştirilmiş, .NET Core tabanlı bir web API projesidir. Proje, temel dijital cüzdan işlevselliğini göstermek amacıyla oluşturulmuştur.

## 🎯 Proje Amacı

Bu case study projesi, aşağıdaki temel amaçları gerçekleştirmek için geliştirilmiştir:

- Clean Architecture prensiplerinin uygulanması
- Modern .NET Core teknolojilerinin kullanımı
- Güvenli ve ölçeklenebilir bir API tasarımı
- Best practice'lerin uygulanması
- SOLID prensiplerinin gösterilmesi

## 🏗️ Proje Yapısı

Proje, aşağıdaki katmanlardan oluşmaktadır:

- **DigitalWallet.Domain**: Temel domain modellerini ve iş mantığı kurallarını içerir
- **DigitalWallet.Application**: Uygulama servislerini ve iş mantığını içerir
- **DigitalWallet.Infrastructure**: Harici servisler ve altyapı bileşenlerini içerir
- **DigitalWallet.Persistence**: Veritabanı işlemleri ve repository implementasyonlarını içerir
- **DigitalWallet.Presentation**: API endpoint'lerini ve controller'ları içerir
- **DigitalWallet.WebApi**: Ana uygulama katmanı ve konfigürasyonları içerir

## 🚀 Mevcut Özellikler

- Clean Architecture prensiplerine uygun tasarım
- JWT tabanlı kimlik doğrulama
- Entity Framework Core ile veritabanı entegrasyonu
- Swagger/OpenAPI desteği
- Katmanlı mimari yapı
- Dependency Injection kullanımı
- Repository Pattern implementasyonu (User, Wallet, Transaction)
- Validation ve error handling mekanizmaları
- Asenkron programlama yaklaşımı
- SOLID prensiplerine uygun kod yapısı

## 🛠️ Kullanılan Teknolojiler

- .NET Core 8.0
- Entity Framework Core
- JWT Authentication
- Swagger/OpenAPI
- SQL Server

## 📋 Sistem Gereksinimleri

- .NET Core 8.0 veya üzeri
- SQL Server

## 🔧 Kurulum

1. Projeyi klonlayın:

```bash
git clone [repository-url]
```

2. Veritabanı bağlantı ayarlarını `appsettings.json` dosyasında yapılandırın.

3. Migration'ları uygulayın:

```bash
dotnet ef database update
```

4. Projeyi çalıştırın:

```bash
dotnet run --project DigitalWallet.WebApi
```

## 📚 API Dokümantasyonu

API dokümantasyonuna Swagger UI üzerinden erişebilirsiniz:

```
https://localhost:5001/swagger
```

## 🔒 Güvenlik Özellikleri

- JWT tabanlı kimlik doğrulama sistemi
- Güvenli şifreleme ve hash işlemleri

## 🔮 Zaman kısıtı yüzünden ekleyemediğim fakat ekleyebileceğim özellikler

Proje, case study kapsamında temel özellikleri içerecek şekilde tasarlanmıştır. İleride geliştirilebilecek potansiyel özellikler:

1. **Ödeme Sistemleri Entegrasyonu**

   - Stripe, PayPal gibi ödeme sistemleri entegrasyonu

2. **Gelişmiş Güvenlik Özellikleri**

   - İki faktörlü kimlik doğrulama (2FA)
   - IP tabanlı güvenlik kontrolleri

3. **Kullanıcı Deneyimi İyileştirmeleri**

   - WebSocket ile gerçek zamanlı bildirimler
   - Push notification desteği

4. **Entegrasyonlar**

   - Banka API entegrasyonları

5. **Performans İyileştirmeleri**

   - Redis cache implementasyonu
   - Mikroservis mimarisine geçiş
   - Load balancing ve scaling özellikleri

6. **Kod Kalitesi İyileştirmeleri**
   - FluentValidation implementasyonu
   - CQRS pattern implementasyonu
   - AutoMapper entegrasyonu
   - Unit test coverage artırımı

## 📝 Notlar

- Bu proje bir case study kapsamında geliştirilmiştir
- Proje, temel dijital cüzdan işlevselliğini göstermek amacıyla oluşturulmuştur
- Kod kalitesi ve best practice'ler göz önünde bulundurularak geliştirilmiştir
