# OOP_PayCalculator
Preview demo
![Alt Text](https://github.com/mrteeson94/OOP_PayCalculator/blob/main/payCalculator.gif)

## Table Content
* [Application Summary](#Application-Summary)

* [Technology & Language Used](#Technology-&-Language-Used)

* [Functionality](#Functionality)

* [UPDATE](#UPDATE)
* 
## Application Progression Summary
PayCalculator is a WPF application simulating a manager user inputting weekly hours worked to obtain the team's payslip details of the week 
which includes gross/net weekly income, super & tax. These calculated metrics can then be saved into a csv spreadsheet for admin use.

This is my first back-end project which demonstrates my understanding in object-oriented programming in utilising classes + objects, 
apply OOP principles such as abstraction, inheritance and polymorphism, ability to use reusable components from Nuget package to 
produce efficient code layout (saving time and ram space usage) and manipulate spreadsheet files such as exporting, updating and deleting CSV files.


## Technology & Language Used
* Framework: .NET framework v5.0/ WPF Application v4.0.30319
* Languages: C# and XAML 
* IDE: Visual Studio 2022 (Community version)
* Components (via Nuget Packages): CsvHelper v30.0.1

## Functionality
* Able to export csv file 
* Implement data binding between model and view data objects to present csv information onto the UI
* Calculate button to calculate the team's payslip (gross & net income, super, tax) 
* Save button feature to save all the team's payslip records onto a csv file for HR/payroll admin use.

 ## UPDATE
* Current: Relative pathing for reading csv files have been added to avoid manual edit when used in different environemnts.
* Features in progress: Calculate button to wipe calculated values and query new payslip for employees in a single app session
* Future features: Select single employee to perform calculation and import csv file containing only their payslip details
