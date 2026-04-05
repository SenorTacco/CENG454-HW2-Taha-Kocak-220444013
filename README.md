# CENG454-HW2 Sky-High Prototype II - Threat Corridor

CENG 454 Game Programming Midterm - Spring 2025-2026

Unity-6 ucus simulator prototipi. HW1 temel ucus kontrolcusu uzerine Threat Corridor senaryosu eklenmistir.

## Senaryo

Pilot pistten havalanir, terrain uzerinde ucarak tehlikeli bolgeye girer.
HUD uyarisi alir, 5 saniye ardindan yerden fuze firlatilir ve fuze ucagi takip eder.
Pilot bolgeden cikarak fuzeyi yok eder ve guvenli inisle gorevi tamamlar.

## Kontroller

- Yukari/Asagi ok: Pitch
- Sol/Sag ok: Yaw
- Q/E: Roll
- Space: Ileri git

## Ozellikler

- Terrain ile kalkis/inis pisti
- Tehlikeli bolge trigger sistemi
- HUD uyari mesajlari
- Gecikmeli fuze firlatma (5sn)
- Homing fuze (gudumlu takip)
- Carpma/patlama ve ses efektleri
- Gorev dongusu (kalkis -> tehlike -> kacis -> inis)

## Teknik

- Unity-6 + C#
- Git LFS kullanildi (.fbx, .png, .wav)
- Klonlamadan once `git lfs install` calistirilmasi lazim
- Single Responsibility Principle uygulanmistir
