const axios = require('axios');
const { v4: uuidv4 } = require('uuid');

// Configuration
const BASE_URL = 'https://tempus-monolith.azurewebsites.net'; // Replace with your actual API base URL
const API_VERSION = '1'; // Replace with your API version
const BATCH_SIZE = 10; // Number of parallel requests

process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = 0;

// Sample data generators
const firstNames = [
    'John', 'Jane', 'Michael', 'Sarah', 'David', 'Emily', 'Robert', 'Jessica', 'William', 'Ashley',
    'James', 'Amanda', 'Christopher', 'Stephanie', 'Daniel', 'Melissa', 'Matthew', 'Nicole', 'Anthony', 'Elizabeth',
    'Mark', 'Linda', 'Steven', 'Barbara', 'Paul', 'Susan', 'Andrew', 'Karen', 'Joshua', 'Nancy',
    'Kenneth', 'Betty', 'Kevin', 'Helen', 'Brian', 'Sandra', 'George', 'Donna', 'Edward', 'Carol',
    'Ronald', 'Ruth', 'Timothy', 'Sharon', 'Jason', 'Michelle', 'Jeffrey', 'Laura', 'Ryan', 'Sarah',
    'Jacob', 'Kimberly', 'Gary', 'Deborah', 'Nicholas', 'Dorothy', 'Eric', 'Lisa', 'Jonathan', 'Nancy',
    'Stephen', 'Karen', 'Larry', 'Betty', 'Justin', 'Helen', 'Scott', 'Sandra', 'Brandon', 'Donna',
    'Benjamin', 'Carol', 'Samuel', 'Ruth', 'Frank', 'Sharon', 'Gregory', 'Michelle', 'Raymond', 'Laura',
    'Alexander', 'Emily', 'Patrick', 'Kimberly', 'Jack', 'Deborah', 'Dennis', 'Dorothy', 'Jerry', 'Lisa',
    'Tyler', 'Nancy', 'Aaron', 'Karen', 'Jose', 'Betty', 'Henry', 'Helen', 'Adam', 'Sandra'
];

const lastNames = [
    'Smith', 'Johnson', 'Williams', 'Brown', 'Jones', 'Garcia', 'Miller', 'Davis', 'Rodriguez', 'Martinez',
    'Hernandez', 'Lopez', 'Gonzalez', 'Wilson', 'Anderson', 'Thomas', 'Taylor', 'Moore', 'Jackson', 'Martin',
    'Lee', 'Perez', 'Thompson', 'White', 'Harris', 'Sanchez', 'Clark', 'Ramirez', 'Lewis', 'Robinson',
    'Walker', 'Young', 'Allen', 'King', 'Wright', 'Scott', 'Torres', 'Nguyen', 'Hill', 'Flores',
    'Green', 'Adams', 'Nelson', 'Baker', 'Hall', 'Rivera', 'Campbell', 'Mitchell', 'Carter', 'Roberts',
    'Gomez', 'Phillips', 'Evans', 'Turner', 'Diaz', 'Parker', 'Cruz', 'Edwards', 'Collins', 'Reyes',
    'Stewart', 'Morris', 'Morales', 'Murphy', 'Cook', 'Rogers', 'Gutierrez', 'Ortiz', 'Morgan', 'Cooper',
    'Peterson', 'Bailey', 'Reed', 'Kelly', 'Howard', 'Ramos', 'Kim', 'Cox', 'Ward', 'Richardson',
    'Watson', 'Brooks', 'Chavez', 'Wood', 'James', 'Bennett', 'Gray', 'Mendoza', 'Ruiz', 'Hughes',
    'Price', 'Alvarez', 'Castillo', 'Sanders', 'Patel', 'Myers', 'Long', 'Ross', 'Foster', 'Jimenez'
];

const groupNames = ['Development Team', 'Marketing Squad', 'Sales Force', 'Design Studio', 'Quality Assurance', 'Data Analytics', 'Customer Support', 'Finance Team', 'HR Department', 'Operations'];

const categoryNames = ['Work Tasks', 'Personal Goals', 'Meeting Notes', 'Project Updates', 'Ideas & Brainstorming', 'Daily Standup', 'Bug Reports', 'Feature Requests', 'Research Notes', 'Training Materials'];

const categoryColors = ['#FF6B6B', '#4ECDC4', '#45B7D1', '#96CEB4', '#FFEAA7', '#DDA0DD', '#98D8C8', '#F7DC6F', '#BB8FCE', '#85C1E9'];

const registrationContents = [
    'Complete project documentation',
    'Review code changes',
    'Attend team meeting',
    'Update project timeline',
    'Fix critical bug',
    'Implement new feature',
    'Test application functionality',
    'Deploy to production',
    'Review user feedback',
    'Update system requirements',
    'Conduct performance analysis',
    'Create technical specification',
    'Optimize database queries',
    'Refactor legacy code',
    'Setup continuous integration'
];

const registrationDescriptions = [
    'High priority task requiring immediate attention',
    'Regular maintenance activity',
    'Collaborative effort with team members',
    'Client-requested enhancement',
    'Security-related improvement',
    'Performance optimization task',
    'Documentation update required',
    'Testing and validation needed',
    'User experience improvement',
    'System monitoring and alerts',
    'Data backup and recovery',
    'Integration with third-party service',
    'Mobile app compatibility',
    'Accessibility compliance check',
    'Code review and quality assurance'
];

// Utility functions
function getRandomElement(array) {
    return array[Math.floor(Math.random() * array.length)];
}

function generateEmail(firstName, lastName) {
    const domains = ['gmail.com', 'yahoo.com', 'outlook.com', 'company.com', 'icloud.com', 'mail.com', 'gmail.ro'];
    return `${firstName.toLowerCase()}.${lastName.toLowerCase()}@${getRandomElement(domains)}`;
}

function generatePhoneNumber() {
    return `+1${Math.floor(Math.random() * 9000000000 + 1000000000)}`;
}

// API Helper class
class TempusAPIClient {
    constructor(baseUrl, version) {
        this.baseUrl = baseUrl;
        this.version = version;
        this.authToken = null;
    }

    async makeRequest(method, endpoint, data = null, isFormData = false, authorizationToken = null) {
        const url = `${this.baseUrl}/api/v${this.version}${endpoint}`;
        console.log(`Making ${method} request to ${url}`);
        const headers = {
            'Content-Type': isFormData ? 'multipart/form-data' : 'application/json; ver=1.0',
        };

        headers['Authorization'] = `Bearer ${authorizationToken || this.authToken}`;

        try {
            const response = await axios({
                method,
                url,
                data,
                headers
            });
            return response.data;
        } catch (error) {
            console.error(`Error making ${method} request to ${endpoint}:`, error.message);
            if (error.response) {
                console.error('Response data:', error.response.data);
            }
            throw error;
        }
    }

    async login(credentials) {
        const result = await this.makeRequest('POST', '/Auth/login', credentials);
        this.authToken = result.resource.authorizationToken;
        return result;
    }

    async createUser(userData) {
        return await this.login(userData);
    }

async createGroup(groupData, authorizationToken = null) {
        const formData = new FormData();
        formData.append('Name', groupData.name);
        formData.append('Members', JSON.stringify(groupData.members));
        formData.append('UserId', groupData.userId);

        return await this.makeRequest('POST', '/Groups', formData, true, authorizationToken);
    }

    async createCategory(categoryData, authorizationToken = null) {
        return await this.makeRequest('POST', '/Categories', categoryData,false, authorizationToken);
    }

    async createCategoryForGroup(groupId, categoryData, authorizationToken = null) {
        return await this.makeRequest('POST', `/Categories?groupId=${groupId}`, categoryData,false, authorizationToken);
    }

    async createRegistration(registrationData, authorizationToken = null) {
        return await this.makeRequest('POST', '/Registrations', registrationData, false, authorizationToken);
    }

    async createRegistrationForGroup(groupId, registrationData, authorizationToken = null) {
        return await this.makeRequest('POST', `/Registrations?groupId?groupId=${groupId}`, registrationData, false, authorizationToken);
    }
}

// Main script class
class DataPopulationScript {
    constructor() {
        this.client = new TempusAPIClient(BASE_URL, API_VERSION);
        this.users = [];
        this.groups = [];
        this.categories = [];
        this.registrations = [];
    }

    async createBatchOfUsers(count, startIndex = 0) {
        const users = [];

        for (let i = 0; i < count; i++) {
            const firstName = getRandomElement(firstNames);
            const lastName = getRandomElement(lastNames);
            const userIndex = startIndex + i;

            const userData = {
                email: generateEmail(firstName, lastName),
                userName: `${firstName}${lastName}${userIndex}`,
                phoneNumber: generatePhoneNumber(),
                photoURL: `https://api.dicebear.com/7.x/avataaars/svg?seed=${firstName}${lastName}${userIndex}`,
                externalId: uuidv4()
            };

            users.push(userData);
        }

        return users;
    }

    async processUserBatch(userBatch, batchIndex) {
        console.log(`Processing user batch ${batchIndex + 1}...`);
        const processedUsers = [];

        for (const userData of userBatch) {
            try {
                console.log(`Creating user: ${userData.userName}`);
                const loginResult = await this.client.createUser(userData);
                const user = {userDetails: loginResult.resource.user, authorizationToken: loginResult.resource.authorizationToken};
                processedUsers.push(user);

                // Create groups for this user

                // Small delay to avoid overwhelming the API

            } catch (error) {
                console.error(`Failed to create user ${userData.userName}:`, error.message);
            }
        }

        return processedUsers;
    }

    getRandomUserIds(users, excludeUserId, count = 3) {
        // Filter out the current user (group owner)
        const availableUsers = users.filter(user => user.userDetails.id !== excludeUserId);

        // If we don't have enough users, return all available users
        if (availableUsers.length <= count) {
            return availableUsers.map(user => user.userDetails.id);
        }

        // Randomly select 'count' number of users
        const selectedUsers = [];
        const availableUsersCopy = [...availableUsers];

        for (let i = 0; i < count; i++) {
            const randomIndex = Math.floor(Math.random() * availableUsersCopy.length);
            selectedUsers.push(availableUsersCopy[randomIndex].userDetails.id);
            availableUsersCopy.splice(randomIndex, 1);
        }

        return selectedUsers.filter(id => id !== excludeUserId); // Ensure the current user is not included 
    }

    async createGroups(users) {
        const userGroups = [];
        for (const user of users) {

            await this.createCategoriesForUser(user);

            for (let i = 0; i < 3; i++) {
                try {
                    let members = this.getRandomUserIds(users, user.userDetails.id, 7);
                    members = members.filter(id => id !== user.id);
                    // Get 2 random users excluding the current user
                    const groupData = {
                        name: `${getRandomElement(groupNames)}`,
                        members: members.join(), // Start with just the user
                        userId: user.userDetails.id
                    };

                    // Note: The API expects FormData for group creation
                    // This is a simplified version - you may need to adjust based on actual API requirements
                    const group = await this.client.createGroup(groupData, user.authorizationToken);
                    console.log(group.resource);
                    userGroups.push({ ...group.resource, userId: user.userDetails.id });

                    // Create categories for this group
                    await this.createCategoriesForUserGroup(user, group.resource);

                } catch (error) {
                    console.error(`Failed to create group ${i + 1} for user ${user.userDetails.userName}:`, error.message);
                }
            }
        }

        return userGroups;
    }

    async createCategoriesForUser(user) {
        console.log(`Creating categories for user: ${user.userDetails.userName}`);
        const groupCategories = [];

        for (let i = 0; i < 5; i++) {
            try {
                const categoryData = {
                    userId: user.userDetails.id,
                    name: `${getRandomElement(categoryNames)} - ${i + 1}`,
                    color: getRandomElement(categoryColors),
                };

                const category = await this.client.createCategory(categoryData, user.authorizationToken);
                this.categories.push(category.resource);

                // Create registrations for this category
                await this.createRegistrationsForCategory(user, category.resource);

            } catch (error) {
                console.error(`Failed to create category ${i + 1} for user ${user.userDetails.userName}:`, error.message);
            }
        }

        return groupCategories;
    }

    async createCategoriesForUserGroup(user, group) {
        console.log(`Creating categories for group: ${group.name}`);
        const groupCategories = [];

        for (let i = 0; i < 5; i++) {
            try {
                const categoryData = {
                    name: `${getRandomElement(categoryNames)} - ${i + 1}`,
                    color: getRandomElement(categoryColors),
                    groupId: group.id,
                    userId: user.userDetails.id
                };

                const groupCategory = await this.client.createCategoryForGroup(group.id, categoryData, user.authorizationToken);
                groupCategories.push(groupCategory);

                // Create registrations for this category
                await this.createRegistrationsForGroupCategory(user, groupCategory, group);

            } catch (error) {
                console.error(`Failed to create category ${i + 1} for group ${group.name}:`, error.message);
            }
        }

        return groupCategories;
    }

    async createRegistrationsForGroupCategory(user, category, group) {
        console.log(`Creating registrations for category: ${category.name}`);
        const categoryRegistrations = [];

        for (let i = 0; i < 7; i++) {
            try {
                const registrationData = {
                    description: getRandomElement(registrationDescriptions),
                    content: getRandomElement(registrationContents),
                    categoryId: category.id
                };

                const registration = await this.client.createRegistrationForGroup(group.id, registrationData, user.authorizationToken);
                categoryRegistrations.push(registration);

            } catch (error) {
                console.error(`Failed to create registration ${i + 1} for category ${category.name}:`, error.message);
            }
        }

        return categoryRegistrations;
    }

    async createRegistrationsForCategory(user, category) {
        console.log(`Creating registrations for category: ${category.name}`);
        const categoryRegistrations = [];

        for (let i = 0; i < 7; i++) {
            try {
                const registrationData = {
                    userId: user.userDetails.id,
                    description: getRandomElement(registrationDescriptions),
                    content: getRandomElement(registrationContents),
                    categoryId: category.id
                };

                console.log(user.authorizationToken);

                const registration = await this.client.createRegistration(registrationData, user.authorizationToken);
                console.log(registration);
                categoryRegistrations.push(registration);

            } catch (error) {
                console.error(`Failed to create registration ${i + 1} for category ${category.name}:`, error.message);
            }
        }

        return categoryRegistrations;
    }

    async run() {
        console.log('Starting Tempus API data population...');
        console.log(`Target: 250 users, 750 groups, 3,750 categories, 26,250 registrations`);

        const totalUsers = 250;
        const batchCount = Math.ceil(totalUsers / BATCH_SIZE);

        try {
            for (let batchIndex = 0; batchIndex < batchCount; batchIndex++) {
                const startIndex = batchIndex * BATCH_SIZE;
                const endIndex = Math.min(startIndex + BATCH_SIZE, totalUsers);
                const currentBatchSize = endIndex - startIndex;

                console.log(`\n=== Batch ${batchIndex + 1}/${batchCount} (Users ${startIndex + 1}-${endIndex}) ===`);

                // Create user data for this batch
                const userBatch = await this.createBatchOfUsers(currentBatchSize, startIndex);

                // Process the batch
                const processedUsers = await this.processUserBatch(userBatch, batchIndex);
                this.users.push(...processedUsers);

                console.log(processedUsers);

                await this.createGroups(processedUsers);

                console.log(`Completed batch ${batchIndex + 1}. Total users created: ${this.users.length}`);

                if (batchIndex < batchCount - 1) {
                    console.log('Waiting before next batch...');
                }
            }

            console.log('\n=== Data Population Complete ===');
            console.log(`Successfully created:`);
            console.log(`- Users: ${this.users.length}`);
            console.log(`- Expected Groups: ${this.users.length * 3}`);
            console.log(`- Expected Categories: ${this.users.length * 3 * 5}`);
            console.log(`- Expected Registrations: ${this.users.length * 3 * 5 * 7}`);

        } catch (error) {
            console.error('Script execution failed:', error);
        }
    }
}

// Usage instructions and script execution
async function main() {
    console.log('Tempus API Data Population Script');
    console.log('==================================');
    console.log('');
    console.log('IMPORTANT: Before running this script, please:');
    console.log('1. Update the BASE_URL constant with your actual API endpoint');
    console.log('2. Update the API_VERSION if needed');
    console.log('3. Ensure your API is running and accessible');
    console.log('4. Install required dependencies: npm install axios uuid');
    console.log('');

    if (BASE_URL === 'https://your-api-domain.com') {
        console.log('⚠️  Please update the BASE_URL before running the script!');
        return;
    }

    const script = new DataPopulationScript();
    await script.run();
}

// Export for module usage
module.exports = {
    DataPopulationScript,
    TempusAPIClient
};

// Run if called directly
if (require.main === module) {
    main().catch(console.error);
}