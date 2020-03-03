# POSE_PROJECT
P.O.S.E is a simple student ASP.Net Core project, which connects patients, doctors and drugstores.
There are 4 types of users - Patient, Doctor, Drug Store and an Admin. All of them can update their profile info, change password and delete account. Users can register as Patient, Doctor and drug store, Admin registration is auto injected in the DB.

Patients can can choose their prefered drug store and doctor.
They also can look for their examination history, history of spend diseases, prescription history, can see what allergies they have. 
Patientis can see the whole sum for drugs. Patients can give doctor and drug store score for their services.
Patients can send message to their prefered doctor and can join POSE Patients chat.

Doctors can make an examination, can appoint additional tests, can do a diagnosis and
can make a prescription with various drugs(POSE automatically checks if drugs contain ingredients that patient is allergic to and 
excludes them from the list), 
They can send the prescription to the patient's prefered drug store. 
If the patient does not have a prefered drug store, the doctor can choose another store for the prescription.
Doctors can send mails to their patients.
Doctors can see patients medical history(examinations, prescriptions, taken drugs).

Drugstores can perform the prescription, calculate the sum and can issue a receipt.
The project also supports #Administration. Administrators can add or delete drugs, tests, drug ingredients and diseases;

The project is implemented via ASP.Net Core 2.2 with the built-in Dependency injection.
For the purpose of the portfolio, the project uses both Razor Pages(for the Identity and the  Admin areas of the project), MVC(for the rest of the project), sections and partial views, AutoMapper, SignalR and SendGrid.
The project uses Microsoft SQL Server as Database Service and Entity Framework Core to access database. The project uses authentication and email confirmation.
AntiForgeryToken, error handling and data validation are also implemented in the project;



                                
