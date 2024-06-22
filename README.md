# Windows Forms App 

## Overview

This project includes a Visual Basic desktop application and an Express.js backend server built with TypeScript. The application allows users to create and view submissions. Submissions are stored in a JSON file on the backend server.

## Desktop App

### Features

- Create new submissions with fields for Name, Email, Phone Number, GitHub repo link, and a stopwatch timer.
- View submissions with navigation buttons to move through entries.
- Keyboard shortcuts for enhanced user interaction.

### Requirements

- Visual Studio (not Visual Studio Code)
- Visual Basic

### Setup and Running


1.**Clone the Repository:**
   git clone https://github.com/yourusername/your-repo-name.git
   

2.**Open the Project:**
   Open Visual Studio.
   Open the solution file (.sln) from the cloned repository.


3.**Run the Application:**
   Press F5 or use the Start button in Visual Studio to run the application.
   

### Code Overview
MainForm.vb: Contains buttons for "View Submissions" and "Create New Submission".


CreateSubmissionForm.vb: Allows users to enter details and submit them.


ViewSubmissionsForm.vb: Allows users to navigate through and view submissions.


## Backend Server


***Features***
API endpoints to handle submissions.
Uses a JSON file (db.json) to store data.

***Requirements***
Node.js
TypeScript

**Setup and Running**

Clone the Repository:
git clone https://github.com/yourusername/your-repo-name.git

Navigate to Backend Directory:
cd your-repo-name/backend

Install Dependencies:
npm install

Compile TypeScript:
npx tsc

Run the Server:
node dist/index.js

### API Endpoints
/ping (GET): Returns { success: true }


/submit (POST): Submits a new entry with name, email, phone, github_link, and stopwatch_time.


/read (GET): Reads a submission by index.
