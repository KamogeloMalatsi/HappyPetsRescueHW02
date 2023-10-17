USE Master
GO


IF EXISTS (SELECT * FROM sys.databases WHERE NAME = 'HappyPetsRescue')
DROP DATABASE HappyPetsRescue
GO


CREATE DATABASE HappyPetsRescue
GO


USE HappyPetsRescue
GO


CREATE TABLE PetType
(
	PetTypeID int Identity(1,1) Primary Key NOT NULL,
	PetTypeName nvarchar(100) NOT NULL
)
GO


CREATE TABLE Gender
(
	GenderID int Identity(1,1) Primary Key NOT NULL,
	GenderName nvarchar(100) NOT NULL
)


CREATE TABLE AdoptionStatus
(
	AdoptionStatusID int Identity(1,1) Primary Key NOT NULL,
	StatusName nvarchar(100) NOT NULL
)


CREATE TABLE [Location]
(
	LocationID int Identity(1,1) Primary Key NOT NULL,
	LocationName varchar(100) NOT NULL
)


CREATE TABLE Breed
(
	BreedID int Identity(1,1) Primary Key NOT NULL,
	BreedName nvarchar(100) NOT NULL,
	PetTypeID int FOREIGN KEY REFERENCES PetType(PetTypeID)
)


CREATE TABLE [User]
(
	UserID int Identity(1,1) Primary Key NOT NULL,
	UserFullName nvarchar(100) NOT NULL,
	UserContactNumber nvarchar(10) NOT NULL
)


CREATE TABLE Donation
(
	DonationID int Identity(1,1) Primary Key NOT NULL,
	DonationAmount decimal(10,2) NOT NULL,
	DonationDate date NOT NULL,
	UserID int FOREIGN KEY REFERENCES [User](UserID)
)


CREATE TABLE Pet
(
	PetID int Identity(1,1) Primary Key NOT NULL,
	PetName nvarchar(100) NOT NULL,
	PetAge int NOT NULL,
	PetStory nvarchar(max) NOT NULL,
	PetWeight decimal(5,2) NOT NULL,
	PetImage varchar(255) NOT NULL,
	GenderID int FOREIGN KEY REFERENCES Gender(GenderID),
	LocationID int FOREIGN KEY REFERENCES [Location](LocationID),
	PetTypeID int FOREIGN KEY REFERENCES PetType(PetTypeID),
	BreedID int FOREIGN KEY REFERENCES Breed(BreedID),
	AdoptionStatusID int FOREIGN KEY REFERENCES AdoptionStatus(AdoptionStatusID),
	UserID int FOREIGN KEY REFERENCES [User](UserID),
)


CREATE TABLE AdoptedPet
(
	AdoptionID int Identity(1,1) Primary Key NOT NULL,
	PetID int references Pet(PetID),
	UserID  int references [User](UserID),
)


INSERT INTO Gender (GenderName) VALUES
('Male'),
('Female'),
('Other')


INSERT INTO AdoptionStatus(StatusName) VALUES
('Available'),
('Adopted')


INSERT INTO [Location] (LocationName) VALUES
('Gauteng'),
('Limpopo'),
('Mpumalanga'),
('Free State'),
('KwaZulu Natal'),
('North West'),
('Northern Cape'),
('Eastern Cape'),
('Western Cape')


INSERT INTO PetType (PetTypeName) VALUES
('Dog'),
('Cat'),
('Bird'),
('Fish')


INSERT INTO Breed(BreedName, PetTypeID) VALUES
('Golden Retriever',1),
('Poodle',1),
('Dachshund',1),
('German Shepherd',1),
('Labrador Retriever',1),
('Maine Coon',2),
('Russian Blue',2),
('Bengal',2),
('Siamese',2),
('Persian',2),
('Parrot',3),
('Cockatiel',3),
('Parakeet',3),
('Cockatoo',3),
('Budgerigar',3),
('Betta',4),
('Goldfish',4),
('Tetra',4),
('Guppy',4),
('Angelfish',4)


INSERT INTO [User] (UserFullName, UserContactNumber) VALUES 
('Thando Nkosi', '0821234567'),
('Jessica Botha', '0799876543'),
('Michael Johnson', '0768765432'),
('Sarah Brown', '0712345678'),
('David Williams', '0834567890'),
('Linda Davis', '0723456789'),
('Peter Taylor', '0798765432'),
('Michelle Clark', '0845678901'),
('Ryan Anderson', '0781234567'),
('Emily Harris', '0839876543')


INSERT INTO Donation (DonationAmount, DonationDate, UserID) VALUES
(1200.00, '2023-07-12',2),
(5000.00, '2023-08-18',5),
(750.00, '2023-08-27',7),
(10500.00, '2023-09-03',2),
(1000.00, '2023-09-14',6),
(2500.00, '2023-09-20',1)


INSERT INTO Pet (PetName, PetAge, PetStory, PetWeight, PetImage, GenderID, LocationID, PetTypeID, BreedID, AdoptionStatusID,UserID) VALUES
('Max',3,
'Max, the playful Golden Retriever, hails from a sunny farm in Gauteng. Known for his friendly nature and love for fetching balls, Max brings endless joy to his family. He is a loyal companion, always ready for an adventure in the nearby park.',
7,'GoldenRetriever.jpg',1,9,1,1,2,2),
('Coco',7,
'Coco, the elegant Poodle, was born and raised in a cozy home in KwaZulu Natal. With a penchant for cuddles and a flair for tricks, Coco is adored by all. She is a clever and loving buddy, making days brighter with every tail wag.',
5,'Poodle.jpeg',2,1,1,2,1,4),
('Bean',5,
'Bean, the affectionate Russian Blue, comes from a loving home in Western Cape. With her soft fur and playful antics, she is a heart-stealer. Bean enjoys lazy afternoons by the window, purring contently in the warmth of the sun.',
4,'RussianBlue.jpg',2,5,2,7,2,7),
('Smokey',10,
'Smokey, the charming Siamese, found his forever home in Gauteng. With striking blue eyes and a curious spirit, he is a true delight. Smokey loves perching on windowsills, watching the world outside, and cuddling up with his favorite humans.'
,5,'Siamese.jpeg',1,1,2,9,1,3),
('Bubba',3,
'Bubba, the talkative Parrot, was adopted in Mpumalanga. Known for mimicking words and tunes, Bubba keeps everyone entertained. He brightens up the room with his vibrant plumage and cheerful personality, bringing laughter and melody to the household.',
1,'Parrot.jpeg',1,3,3,11,1,10),
('Ozzy',5,
'Ozzy, the playful Cockatiel, was welcomed into a lively home in Free State. With her lively chirps and adorable crest, she is a joy to have around. Ozzy loves perching on shoulders, spreading happiness and chirping along to tunes.',
1,'Cockatiel.jpg',2,4,3,12,2,1),
('Bubbles',1,
'Bubbles, the vibrant Betta, resides in a small aquarium in North West. With her colorful fins and graceful movements, she is a mesmerizing sight. Bubbles enjoys exploring her aquatic world and capturing the attention of all who pass by.',
0.1,'Betta.jpg',2,9,4,16,1,9),
('Goldie',1,
'Goldie, the charming Goldfish, swims happily in a tank in Northern Cape. Adored for his graceful glide and vibrant hues, Goldie is a soothing presence. He is a low-maintenance yet captivating pet, bringing a sense of tranquility to the home.',
0.2,'Goldfish.jpg',1,6,4,17,2,6)


INSERT INTO AdoptedPet(PetId, UserID) VALUES
(1,3),
(3,1),
(6,8),
(8,6)