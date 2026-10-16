<div align="center">

# ⚽ Serie A Football Platform

### 🏆 Modern Serie A Match & Statistics Management System

<img src="https://capsule-render.vercel.app/api?type=waving&color=0:0f3d2e,50:16a085,100:00ff88&height=220&section=header&text=SERIE%20A&fontSize=65&fontColor=ffffff&animation=fadeIn&fontAlignY=35&desc=Football%20Match%20Management%20Platform&descAlignY=58&descSize=20" width="100%"/>

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core/)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-68217A?style=for-the-badge&logo=.net&logoColor=white)](https://learn.microsoft.com/ef/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)
[![MVC](https://img.shields.io/badge/ASP.NET-MVC-0b5d3b?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet/mvc)

<br>

> 🟢 **Fixtures • Live Match Details • Standings • Statistics • Team Management**

</div>

---

## 🚀 About The Project

**Serie A Football Platform**, Serie A futbol organizasyonunu modern bir web arayüzü üzerinden yönetmek ve görüntülemek amacıyla geliştirilmiş bir **ASP.NET Core Web API & MVC projesidir.**

Proje kapsamında;

- ⚽ Maç fikstürleri
- 🏆 Dinamik puan durumu
- 📊 Maç istatistikleri
- 🥅 Gol olayları
- 🟨 Sarı / kırmızı kartlar
- 🔄 Oyuncu değişiklikleri
- 🏟️ Takım ve stadyum bilgileri
- 🖼️ Takım logoları
- 🛠️ CRUD işlemleri

tek bir platform üzerinden yönetilebilir.

🎯 Projenin temel amacı yalnızca CRUD işlemleri gerçekleştirmek değil; **Web API geliştirme, API tüketimi, ilişkisel veri modelleme, DTO kullanımı ve maç sonuçlarından dinamik istatistikler üretme** konularında pratik kazanmaktır.

---

## 🎓 Project Background

Bu proje, **M&Y Yazılım Eğitim Akademi Danışmanlık – 11. Dönem Full Stack .NET Bootcamp** kapsamında geliştirilmiştir.

### 👨‍💻 Geliştirici

**Çağan Derindere**

🎓 11. Dönem Full Stack .NET Bootcamp Öğrencisi

🏫 **M&Y Yazılım Eğitim Akademi Danışmanlık**

Bu proje ile birlikte **ASP.NET Core Web API, MVC, Entity Framework Core, SQL Server, REST API, DTO, AutoMapper, LINQ ve HttpClient** teknolojileri üzerinde uygulamalı olarak çalışılmıştır.

---

## ✨ Features

| ⚽ Feature | 📌 Description |
|---|---|
| 📅 Fixture | Haftalara göre maç fikstürü |
| 🏆 Standings | Maç sonuçlarından otomatik hesaplanan puan tablosu |
| ⚽ Match Details | Detaylı maç merkezi |
| 🥅 Goals | Gol olayları ve dakika bilgileri |
| 🟨 Cards | Sarı / kırmızı kart bilgileri |
| 🔄 Substitutions | Oyuncu değişiklikleri |
| 🏟️ Team Info | Takım ve stadyum bilgileri |
| 🖼️ Team Logos | Dinamik takım logoları |
| 📊 Statistics | Maçlardan hesaplanan istatistikler |
| 🛠️ Admin Panel | Maç ekleme, güncelleme ve silme |
| 🔌 REST API | Verilerin API üzerinden sunulması |
| 📱 Responsive UI | Mobil ve masaüstü uyumlu tasarım |

---

## 🧠 Dynamic Standings

Puan tablosu maç sonuçlarından **otomatik olarak hesaplanmaktadır.**

```text
🏆 Galibiyet      → 3 Puan
🤝 Beraberlik     → 1 Puan
❌ Mağlubiyet     → 0 Puan
```

Sistem maç sonuçlarını analiz ederek takımların:

- Oynadığı maç
- Galibiyet
- Beraberlik
- Mağlubiyet
- Attığı gol
- Yediği gol
- Averaj
- Toplam puan

bilgilerini dinamik olarak hesaplar.

---

## 🛠️ Technologies

### Backend

- ⚙️ .NET 8
- 🚀 ASP.NET Core Web API
- 🗄️ Entity Framework Core
- 🛢️ Microsoft SQL Server
- 🔗 REST API
- 🔄 LINQ
- 📦 DTO
- 🗺️ AutoMapper
- 🌐 HttpClient

### Frontend

- 🎨 ASP.NET Core MVC
- 🧩 Bootstrap
- ⚡ JavaScript
- 🖥️ Razor Views
- 📱 Responsive Design

---

## 🏗️ Project Architecture

Proje, **Web API + MVC** yaklaşımı kullanılarak geliştirilmiştir.

```text
                    ┌──────────────────────┐
                    │      MVC Client      │
                    │   Razor + Bootstrap  │
                    └──────────┬───────────┘
                               │
                               │ HttpClient
                               ▼
                    ┌──────────────────────┐
                    │    ASP.NET Core      │
                    │       Web API        │
                    └──────────┬───────────┘
                               │
                    ┌──────────▼───────────┐
                    │ Entity Framework Core│
                    └──────────┬───────────┘
                               │
                    ┌──────────▼───────────┐
                    │     SQL Server       │
                    └──────────────────────┘
```

Bu yapı sayesinde frontend ve backend birbirinden ayrılarak daha düzenli ve sürdürülebilir bir yapı oluşturulmuştur.

---

## 🔌 API & Data Management

Web API tarafında takım, maç ve maç olayları gibi veriler REST prensiplerine uygun endpoint'ler üzerinden yönetilmektedir.

Temel işlemler:

```text
GET     → Verileri listeleme
GET     → Detay görüntüleme
POST    → Yeni veri ekleme
PUT     → Veri güncelleme
DELETE  → Veri silme
```

MVC tarafında ise **HttpClient** kullanılarak Web API'den alınan veriler kullanıcı arayüzüne aktarılmıştır.

---

## 📊 Match Management

Maç detayları üzerinden aşağıdaki bilgiler görüntülenebilir:

```text
⚽ Home Team
⚽ Away Team

🥅 Goals
🟨 Yellow Cards
🟥 Red Cards
🔄 Substitutions

⏱️ Match Minute
📊 Match Statistics
🏟️ Stadium Information
```

Bu sayede kullanıcı yalnızca maç sonucunu değil, karşılaşmanın detaylarını da görüntüleyebilir.

---

## 🗃️ Database

Projenin veri yönetiminde **Microsoft SQL Server** ve **Entity Framework Core** kullanılmıştır.

Veritabanında temel olarak;

- Teams
- Matches
- Goals
- Cards
- Substitutions
- Stadiums

gibi ilişkili yapılar üzerinden futbol verileri yönetilmektedir.

Entity Framework Core sayesinde veritabanı işlemleri ORM yaklaşımıyla gerçekleştirilmiştir.

---

## 🎯 What I Practiced

Bu proje sayesinde özellikle aşağıdaki konularda uygulamalı deneyim kazandım:

- ✅ ASP.NET Core Web API geliştirme
- ✅ RESTful API tasarımı
- ✅ CRUD operasyonları
- ✅ Entity Framework Core
- ✅ SQL Server veritabanı yönetimi
- ✅ İlişkisel veri modelleme
- ✅ DTO kullanımı
- ✅ AutoMapper
- ✅ LINQ sorguları
- ✅ HttpClient ile API tüketimi
- ✅ ASP.NET Core MVC
- ✅ Bootstrap ile responsive arayüz
- ✅ JavaScript ile frontend işlemleri
- ✅ Dinamik veri ve istatistik hesaplama

---

## 📸 Project Preview

> Projenin arayüzünden örnek görüntüler:

<img src="https://hizliresim.com/iujbf0d1" width="100%" alt="Serie A Football Platform"/>

---

## 👨‍💻 Developer

<div align="center">

### Çağan Derindere

🎓 **M&Y Yazılım Eğitim Akademi Danışmanlık**  
🚀 **11. Dönem Full Stack .NET Bootcamp**

Backend ve Full Stack .NET teknolojileri üzerine çalışmalarını sürdürüyor.

</div>

---

## 🔗 GitHub

📂 Projenin kaynak kodlarına GitHub üzerinden ulaşabilirsiniz.

**MYAcademy Web API / Serie A Football Platform**

---

## 📌 Bootcamp

Bu proje:

**M&Y Yazılım Eğitim Akademi Danışmanlık**  
**11. Dönem Full Stack .NET Bootcamp**

kapsamında geliştirilmiştir.

---

<div align="center">

### ⚽ Code. Build. Learn. Repeat. 🚀

**ASP.NET Core • Web API • MVC • SQL Server • Entity Framework Core**

</div>
