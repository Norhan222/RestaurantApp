# 🍔 Foodia - Restaurant Web App

## 🚀 Overview
*Foodia* is a full-stack restaurant web application built with *ASP.NET MVC, following **Clean Architecture* principles for scalability, maintainability, and clear separation of concerns.

The app allows users to browse menu items, filter them by category, view offers, and add items to the cart — even without logging in, thanks to *Redis In-Memory Database*.

An *Admin Dashboard* is also included to manage categories, menu items, and customer orders efficiently.

---

## ✨ Features
- 🏗 *Clean Architecture* implementation (UI, Application, Domain, Infrastructure layers).
- 🔐 *ASP.NET Identity* integration for authentication, registration, and role-based access control.
- ⚡ *Redis In-Memory Database* used for:
  - Caching frequently accessed data (menu items, categories)
  - Allowing users to add to cart without logging in
- 🧩 *Category filtering* using *Isotope.js* with smooth animations.
- 🛒 Interactive *cart system* with live updates.
- 📊 *Admin Dashboard* to manage menu items, categories, and orders.
- 🎨 *Modern responsive design* using *Bootstrap 5* with a custom orange & black theme.
- 💾 Data persistence using *Entity Framework + SQL Server*.
---
## 🧱 Architecture
The project follows *Clean Architecture*, which separates the code into four main layers:
