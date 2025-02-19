# MVC Sound to Database Converter

This project provides a solution to convert every instance of `sound.add` from an autorun into a database format, and then export it as a `.manifest` file for use in the Source Engine.

## Overview

The goal of this project is to automate the process of converting sound assets referenced in an autorun file into a more manageable and structured database. The database generated can then be exported into a `.manifest` file, which is commonly used by the Source Engine to load and manage sound resources efficiently.

## Features

- **Sound Extraction:** Parse and identify every instance of `sound.add` in the autorun file.
- **Database Conversion:** Convert the sound references into a structured database format.
- **Export Manifest:** Export the data as a `.manifest` file for use with the Source Engine.
- **MVC Architecture:** Implements the Model-View-Controller (MVC) design pattern to separate concerns and maintain code organization.

## Prerequisites

Before running the project, ensure you have the following installed:

- [Node.js](https://nodejs.org/) (v14 or later)
- A database system (e.g., SQLite, MySQL, PostgreSQL) for storing sound data
- Source Engine-compatible environment for using the `.manifest` file

## Installation

1. Clone this repository:

    ```bash
    git clone https://github.com/yourusername/sound-to-db-converter.git
    cd sound-to-db-converter
    ```

2. Install dependencies:

    ```bash
    npm install
    ```

3. Set up your database. You can configure it in the `config/database.js` file.

## Usage

### Step 1: Parsing Autorun File

To convert a `.autorun` file to a database:

```bash
node parseAutorun.js path/to/autorun/file
