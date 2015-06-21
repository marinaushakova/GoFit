use gofitdb;

INSERT INTO type([name],[measure]) VALUES('distance', 'miles');
INSERT INTO type([name],[measure]) VALUES('quantity', 'unit');
INSERT INTO type([name],[measure]) VALUES('duration', 'seconds');
INSERT INTO type([name],[measure]) VALUES('duration', 'minutes');
INSERT INTO type([name],[measure]) VALUES('duration', 'hours');

INSERT INTO category([name], [description]) VALUES('endurance', 'Endurance workouts help keep your heart, lungs, and circulatory system healthy.');
INSERT INTO category([name], [description]) VALUES('strength', 'Strength workouts build muscle size and power');
INSERT INTO category([name], [description]) VALUES('flexibility', 'Flexibility workouts stretch your muscles and help protect you body from exercise incurred injuries');

INSERT INTO [user]([username],[password],[fname],[lname],[is_male],[is_admin]) VALUES('admin', '3c1c88f0b0fec9b5f539c3d6b0577bd138bd157d604125a53e60e35cf940a5fe', 'Bob', 'Jones', 1, 1);
INSERT INTO [user]([username],[password],[fname],[lname],[is_male],[is_admin]) VALUES('admin2', 'cf312ac87c1b48968a08cb1b2809a5d0223668ad334ac322ca7f7ab3bce08929', 'Jane', 'Forsythe', 0, 1);

INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('hunts', '5faf9c3e4b1fe468cb6fc9e7cc3bd1eb93f3cad399d45e9a358a14c2c7b483d6', 'Sharon', 'Hunt', 0, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('SharonArmstrong', 'b78ca34b54a6da5d0b8d95a584276450f3c6dbdaafa0d6b4c26bdf893f234573', 'Sharon', 'Armstrong', 0, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('dunnj', 'c98f2f0ce893c007f05c4c2904a5f66e72dd2ea2b8824d3927a3a763be4fb235', 'Joseph', 'Dunn', 1, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('AnneBailey', '46bab764e7e864db5484015e720e5170a6b9f32fb2ab5b1af3cbe7b32dcc9659', 'Anne', 'Bailey', 0, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('FrankSmith', '4f01f4fcf95a540b2623301119fd13a914c91a669cae2df5beb284bed4bc166e', 'Frank', 'Smith', 1, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('DorothyMoreno', '2acf57de58e9bc67475e872727864dd07c11268e652cdd5649de9d7a94c0ec71', 'Dorothy', 'Moreno', 0, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('CharlesDavis', '9ddc3eb834f671e17f1bac2f0b19ae4b1f3befec7134cd95ccc6f57c7864f575', 'Charles', 'Davis', 1, 0);

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

INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-15', 'https://www.youtube.com/watch?v=xKy-l9Pf0cQ', 'O-Pose', 'O-Pose');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-15', 'https://www.youtube.com/watch?v=JY9SlansmJ4', 'Y-Pose', 'Y-Pose');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-15', 'https://www.youtube.com/watch?v=wmVMcHeqoZc', 'I-Pose', 'I-Pose');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-15', 'https://www.youtube.com/watch?v=Aa6zdmje-c4', 'Cobra stretch', 'Cobra stretch');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-12', 'https://www.youtube.com/watch?v=ZiNXOE5EsZw', 'Cat Stretch', 'Cat Stretch');

INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-13', 'https://www.youtube.com/watch?v=7rRWy7-Gokg', 'Reverse Crunch', 'Reverse Crunch');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-13', 'https://www.youtube.com/watch?v=FrFyUbxs1uQ', 'Side Crunch', 'Side Crunch');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-13', 'https://www.youtube.com/watch?v=5laCNeFnKdE', 'One-sided Crunch', 'One-sided Crunch');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-13', 'https://www.youtube.com/watch?v=VSp0z7Mp5IU', 'Inchworm ', 'Inchworm');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-12', 'https://www.youtube.com/watch?v=ihG0E_4_tCM', 'Scissor Lifts', 'Scissor Lifts');

INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-10', 'https://www.youtube.com/watch?v=rXAbcneAr3I', 'Glute Bridge March', 'Glute Bridge March');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-10', 'https://www.youtube.com/watch?v=HPnFTmjjDDA', 'Hollow Rock', 'Hollow Rock');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-10', 'https://www.youtube.com/watch?v=XMxHTNPPgxM', 'Plank', 'Plank');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-10', 'https://www.youtube.com/watch?v=jGQ8_IMPQOY', 'Squat ', 'Squat ');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-12', 'https://www.youtube.com/watch?v=U4s4mEQ5VqU', 'Squat Jump', 'Squat Jump');

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

INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout8', 'Some really long description. Some really long description. Some really long description. Some really long description. Some really long description. Some really long description. Some really long description. Some really long description. Some really long description. ', 2, 1, GETDATE());
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout9', 'desc9', 3, 3, '2015-06-12');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout10', 'desc10', 3, 3, '2015-06-12');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout11', 'desc11', 1, 3, '2015-06-12');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout12', 'desc12', 3, 1, '2015-06-12');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout13', 'desc13', 2, 2, '2015-06-12');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout14', 'desc14', 2, 2, '2015-06-12');

INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout15', 'desc15', 2, 3, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout16', 'desc16', 3, 3, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout17', 'desc17', 2, 2, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout18', 'desc18', 2, 3, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout19', 'desc19', 3, 2, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout20', 'desc20', 1, 3, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout21', 'desc21', 1, 2, '2015-06-13');

INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout22', 'desc22', 3, 2, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout23', 'desc23', 2, 3, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout24', 'desc24', 2, 3, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout25', 'desc25', 3, 3, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout26', 'desc26', 3, 3, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout27', 'desc27', 3, 2, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout28', 'desc28', 3, 3, '2015-06-14');


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

