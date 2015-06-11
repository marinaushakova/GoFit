use gofitdb;

INSERT INTO type([name],[measure]) VALUES('distance', 'miles');
INSERT INTO type([name],[measure]) VALUES('quantity', 'unit');
INSERT INTO type([name],[measure]) VALUES('duration', 'seconds');
INSERT INTO type([name],[measure]) VALUES('duration', 'minutes');
INSERT INTO type([name],[measure]) VALUES('duration', 'hours');

INSERT INTO category([name], [description]) VALUES('endurance', 'Endurance workouts help keep your heart, lungs, and circulatory system healthy.');
INSERT INTO category([name], [description]) VALUES('strength', 'Strength workouts build muscle size and power');
INSERT INTO category([name], [description]) VALUES('flexibility', 'Flexibility workouts stretch your muscles and help protect you body from exercise incurred injuries');

INSERT INTO [user]([username],[password],[fname],[lname],[is_male],[is_admin]) VALUES('admin', 'admin', 'Bob', 'Jones', 1, 1);
INSERT INTO [user]([username],[password],[fname],[lname],[is_male],[is_admin]) VALUES('admin2', 'admin2', 'Jane', 'Forsythe', 0, 1);

INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('hunts', 'hunts', 'Sharon', 'Hunt', 0, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('SharonArmstrong', 'SharonArmstrong', 'Sharon', 'Armstrong', 0, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('dunnj', 'dunnj', 'Joseph', 'Dunn', 1, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('AnneBailey', 'AnneBailey', 'Anne', 'Bailey', 0, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('FrankSmith', 'FrankSmith', 'Frank', 'Smith', 1, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('DorothyMoreno', 'DorothyMoreno', 'Dorothy', 'Moreno', 0, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('CharlesDavis', 'CharlesDavis', 'Charles', 'Davis', 1, 0);

INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=bJ3Ogh5mFE4', 'Standard push-ups', 'Standard Push-ups');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=pUJnPMjYLxU', 'An easier variation of the standard push-up', 'Knee Down Push-ups');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=Ir8IrbYcM8w', 'Pull-ups', 'Pull-ups');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=_71FpEaq-fQ', 'Chin-ups', 'Chin-ups');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=COKYKgQ8KR0', 'Lunge', 'Lunge');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=7mDWDlzFobQ', 'Walking lunge', 'Walking Lunge');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=y7Iug7eC0dk', 'Jumping lunge', 'Jumping Lunge');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=UOGvtqv856A', 'Mountain climber plank', 'Mountain Climber Plank');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=NXr4Fw8q60o', 'Side plank', 'Side Plank');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=HJLE_VQ3Knc', 'Oblique V-up', 'Oblique V-up');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=jDwoBqPH0jk', 'Sit-up', 'Sit-up');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=MKmrqcoCZ-M', 'Stomach crunch', 'Stomach Crunch');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (1, 1, GETDATE(), 'https://www.youtube.com/watch?v=e7m205ZIxBE', 'Running', 'Running');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=9PxkxHxGRvU', 'T-Pose', 'T-Pose');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=i4XOoQUtaCU', 'W-Pose', 'W-Pose');

INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Upper Body Builder 1', 'Works out your back and chest', 2, 1, GETDATE());
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Leg Workout 1', 'Works out your legs and lower back', 2, 1, GETDATE());
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Running Core Workout', 'Build endurance and core strength', 1, 1, GETDATE());
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Endurance Endurance Endurance', 'Build endurance and core strength', 1, 1, GETDATE());
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Sprints', 'Build endurance and leg strength', 1, 1, GETDATE());
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Running Upper Body Workout', 'Build endurance and upper body strength', 1, 1, GETDATE());
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Ab Workout', 'Build core strength', 2, 1, GETDATE());

INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (1, 2, 1, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (1, 1, 2, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (1, 3, 3, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (1, 4, 4, 10);

INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (2, 5, 1, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (2, 6, 2, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (2, 7, 3, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (2, 12, 4, 40);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (2, 5, 5, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (2, 6, 6, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (2, 7, 7, 10);

INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (3, 13, 1, .5);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (3, 12, 2, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (3, 13, 3, .5);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (3, 12, 4, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (3, 11, 5, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (3, 10, 6, 10);

INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (4, 13, 1, 1);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (4, 14, 2, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (4, 15, 3, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (4, 8, 4, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (4, 9, 5, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (4, 13, 6, 2);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (4, 14, 7, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (4, 15, 8, 20);

INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (5, 13, 1, .25);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (5, 13, 2, .25);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (5, 13, 3, .25);

INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (6, 3, 1, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (6, 13, 2, 2);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (6, 1, 3, 30);

INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (7, 10, 1, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (7, 11, 2, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (7, 12, 3, 20);

