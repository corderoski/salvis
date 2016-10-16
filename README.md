# README #

**Salvis**, it's a project for a personal financial manager and buddy (assistant).

It's a Portal where you can to add Savings, Debts (Goals) or Recurrents payments for keeping a record and stats about them. Manages an internal notification and messaging module for letting know the user about incoming transactions.

Also, it has the design of a wizard or budget planner which prints out a PDF document with feedback about the inputs (income, outcome, extra bills, etc.).



### How do I get set up? ###

* Download
* Look for the /db/schema.sql
* Then can use the /db/data.sql for inserting some basic data
* Check for connectionStrings & configurations
* Compile & Run


### Technologies & comments ###

* Use: ASP.NET MVC 4 with the Web API
* JavaScript, jQuery, KnockoutJS (just few scripts)
* Has a Library for connecting to the some APIs like iTunes, Twitter and others feeds
* Use a pre-assembled template
* NUnit + Autofac + AutoFixture for a TDD approach. Most of the tests are missing.


### Screen ###

![ScreenShot](https://raw.githubusercontent.com/corderoski/salvis/master/SalvisHome.JPG)
