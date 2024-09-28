# Personal Data Tool

## Overview
This project is a C# application designed for indexing personal documents and messages across platforms and devices, making them easily taggable and searchable. It integrates with Meilisearch for indexing, Tika for text extraction, and includes features for migrations and classification.

## Known Problems
When running this in your IDE on mac I always use Rider. I run into problems with the binary for Meilisearch binary not being trusted by mac. You will have to build the application and then navigate into the /PDT.CLI/bin/net8.0/ folder and run the `xattr -d com.apple.quarantine meilisearch` command.

## Features
- **Repository Management**: Add, remove, and manage repositories.
- **Document Management**: Add and remove documents within repositories.
- **Text Extraction**: Extract text from documents using Tika.
- **Cross-Platform Indexing**: Index documents and messages from various platforms and devices.
- **Tagging and Searching**: Easily tag and search through indexed content.
- **Migrations**: Support for migrating data between different versions or systems.
- **Classification**: Classify documents and messages for better organization and retrieval.

## Prerequisites
- .NET 8.0
- Mac Silicone
- Mac OS

## Technology Stack
- **Meilisearch**: For indexing and searching documents.
- **TikaOnDotNet**: For text extraction from documents.
- **MAUI**: For building cross-platform applications.

## Installation
1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/your-repo.git
    ```
2. Navigate to the project directory:
    ```sh
    cd your-repo
    ```
3. Restore the dependencies:
    ```sh
    dotnet restore
    ```
4. Install the binary from https://github.com/meilisearch/meilisearch/releases/tag/v1.10.2 into /PDT/meilisearch (meilisearch being the file name).

## Configuration
1. Configure Meilisearch settings in `appsettings.json`:
    ```json
    {
      "MeiliConfiguration": {
        "MeiliRepositoryIndex": "your-repository-index"
      }
    }
    ```
2. Configure Tika settings in `appsettings.json`:
    ```json
    {
      "TikaConfiguration": {
        "TikaThreadPoolSize": 5
      }
    }
    ```

## Logging
The application uses `Microsoft.Extensions.Logging` for logging. Configure the logging settings in `appsettings.json` as needed.

## Contributing
1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -am 'Add new feature'`).
4. Push to the branch (`git push origin feature-branch`).
5. Create a new Pull Request.

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.


Here's the updated roadmap with document tagging and migrations:

---

## Feature Roadmap

### Index Management
- **[ ] Mount/Unmount Indexes:**
  Implement a feature to compress and decompress indexes for storage efficiency. This will allow indexes to be preserved without deletion, though the process may take time.

- **[ ] Export/Import Indexes:**
  Provide the ability to export indexes to an external location and re-import them as needed.

- **[ ] Delete Repositories & Indexes:**
  Allow users to delete repositories and automatically remove their associated indexes.

---

### Document Management
- **[ ] Document Tagging:**
  Add support for tagging documents for better organization, searchability, and categorization across repositories.

- **[ ] Document Migrations:**
  Implement a migration system for moving documents between repositories, ensuring metadata and indexes are preserved.

---

### Monitoring & Performance
- **[ ] Resource Monitoring:**
  Add real-time monitoring for CPU, memory, network usage, and database size to ensure performance and system health.

---

### Search & Navigation
- **[ ] Search with Autocomplete:**
  Implement a search feature with autocomplete functionality to assist users in finding specific fields more easily.

- **[ ] File System Search UI:**
  Build a user-friendly file system interface for viewing and searching files across multiple repositories.

---

### Onboarding & Integration
- **[ ] Easy Onboarding:**
  Streamline the onboarding process with support for popular file systems and platforms:
  - Windows Local File System
  - Mac Local File System
  - Linux Local File System
  - Google Drive
  - Dropbox
  - OneDrive
  - Discord
  - Pixlr

---

### Security
- **[ ] Recovery Code System:**
  Implement a secure recovery code system for logging in and generating API keys, providing an extra layer of protection for user data.

---

### Text Content Indexing
- **[ ] Optional Text Indexing (High Resource Usage):**
  Add the ability to enable/disable text content indexing via Apache Tika, including OCR for searching the content of files. Note: This feature may consume significant resources.

---

Feel free to contribute or suggest new features!
