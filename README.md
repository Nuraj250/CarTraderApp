# CarTraderApp

## Project Description
**CarTraderApp** is a car trading platform that allows users to browse, order, and manage cars and car parts. The application also provides features for user registration, login, and managing user profiles. Administrators can manage cars, parts, and orders, and generate reports.

## Features
- User registration, login, and profile management
- Admin dashboard for managing cars, car parts, and orders
- Buy now and cart functionality for users
- Password reset with OTP validation
- Custom message boxes for enhanced user interaction

## Technologies Used
- C# (.NET Framework)
- Windows Forms for user interface
- Entity Framework for database management
- MSSQL as the database

## Project Structure
- **ApplicationDBContext.cs**: Handles database context and interactions with the MSSQL database.
- **AdminDashboard.cs, CustomerHomePage.cs**: Main forms for admin and customer interaction.
- **BuyNowControl.cs, CartControl.cs**: Controls for managing purchases and cart functionalities.
- **PasswordHelper.cs**: Helper class for managing password operations, including reset.
- **Validator.cs**: Input validation utilities.

## Getting Started

### Prerequisites
- Visual Studio 2019 or later
- .NET Framework 4.7 or later
- MSSQL Server

### Installation
1. Clone or download the repository.
2. Open the solution `CarTraderApp.sln` in Visual Studio.
3. Restore NuGet packages (if required).
4. Update the database connection string in `ApplicationDBContext.cs` to match your environment.
5. Build and run the application from Visual Studio.

### Running the Application
- **Admin Login**: Use the admin dashboard to manage car inventory, orders, and user accounts.
- **Customer Interface**: Customers can browse cars, add items to their cart, and place orders.

## Project Structure Overview
- **bin/**: Contains compiled binaries and executable files.
- **obj/**: Contains object files and intermediate compilation results.
- **Resources/**: Contains application resources (images, strings, etc.).
- **Properties/**: Contains project-wide settings, including assembly info.
- `*.csproj`: Project file for configuring build settings.

## Screenshots
(You can add screenshots of the application interfaces)

## Contributing
Please feel free to submit pull requests or open issues for improvements.
