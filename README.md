# 🚁 Drone Management System - Windows Programming

> 📚 A project developed for the *Windows Programming* course.  
> 🛠️ Built with **C# WinForms** for managing a professional drone rental and sales service.

## 📌 Project Description

This application simulates the workflow of a company that provides **drone camera rental and sales services** for a wide range of customer needs, such as:

- **Events** (fashion shows, music concerts, festivals)
- **Aerial cinematography**
- **Terrain surveying and construction site monitoring**
- **Design and cable infrastructure supervision**

Customers are classified into two types:
- **Individuals**
- **Organizations**

Each customer record includes:
- Name
- Address
- Customer type
- Contact details (phone number and email)


## 🔄 Rental & Purchase Process

1. **Customer submits a request** for renting or purchasing drones.
2. The system **analyzes and recommends** suitable drone types, quantities, and service options.
3. A **contract** is created containing:
   - Contract ID
   - Customer ID
   - Contract type (rent or purchase)
   - Start and end dates
   - Total value
   - Contract status (processing, completed, or canceled)

---

## 📦 Drone Information

Each drone has:
- Drone ID
- Name
- Type (e.g., filming, surveying, formation flying)
- Rental/Purchase price
- Current status (available, rented, maintenance)

---

## ⚠️ Damage & Compensation Policy

- Damage levels: **Minor**, **Moderate**, **Severe**
- Compensation rates: **10%**, **50%**, or **100%** of the drone's value
- Policy automatically applies based on damage reports

---

## 🎁 Promotion System

The system supports automatic discount programs for:
- Loyal customers
- Bulk rentals
- Long-term rentals

---

## 🧑‍🔧 Technical Staff Management

Technical employees are managed by:
- Staff ID
- Name
- Role (installation, maintenance, consultation)
- Work status (busy, available)

Their responsibilities include:
- On-site drone setup
- Regular maintenance
- Technical support and usage consultation


## 🚀 Features

- 📋 Contract management
- 📦 Drone inventory tracking
- 🧑‍💼 Customer classification and records
- ⚙️ Technical staff assignment
- 📊 Automated analytics and business logic
- 🔐 Windows Form UI with intuitive user experience


## 💻 Technologies Used

- **C#**
- **WinForms**
- **ADO.NET** for data access
- **MSSQL** (local database)


## 📁 How to Run
Clone the repository: git clone https://github.com/your-username/drone-management-winform.git
