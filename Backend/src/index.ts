import express from 'express';
import bodyParser from 'body-parser';
import fs from 'fs';
import path from 'path';

const app = express();
const PORT = 3000;

// Middleware
app.use(bodyParser.json());

// Database file path
const dbFilePath = path.join(__dirname, 'db.json');

// Ensure the db.json file exists
if (!fs.existsSync(dbFilePath)) {
    fs.writeFileSync(dbFilePath, JSON.stringify({ submissions: [] }));
}

// Helper function to read the database
const readDatabase = (): any => {
    const data = fs.readFileSync(dbFilePath, 'utf8');
    return JSON.parse(data);
};

// Helper function to write to the database
const writeDatabase = (data: any): void => {
    fs.writeFileSync(dbFilePath, JSON.stringify(data, null, 2));
};

// Endpoints
app.get('/ping', (req, res) => {
    res.json({ success: true });
});

app.post('/submit', (req, res) => {
    const { name, email, phone, github_link, stopwatch_time } = req.body;
    const newSubmission = { name, email, phone, github_link, stopwatch_time };

    const db = readDatabase();
    db.submissions.push(newSubmission);
    writeDatabase(db);

    res.json({ success: true });
});

app.get('/read', (req, res) => {
    const index = parseInt(req.query.index as string, 10);

    const db = readDatabase();
    if (index >= 0 && index < db.submissions.length) {
        res.json({ success: true, submission: db.submissions[index] });
    } else {
        res.status(404).json({ success: false, message: 'Submission not found' });
    }
});

// Start the server
app.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`);
});
