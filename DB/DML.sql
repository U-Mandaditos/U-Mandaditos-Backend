-- Inserts to Locations table
INSERT INTO [Locations] (Name, Latitude, Longitude, Active, Description)
VALUES ('A. J Lindo', 14.084703, -87.162745, 1, 'Frente al Rótulo -Auditorium'),
       ('Registro', 14.084073, -87.163047, 1, 'Frente al Rótulo -Registro'),
       ('Biblioteca', 14.08555, -87.16324, 1, 'Frente a la entrada principal de la Librería'),
       ('F1', 14.08488, -87.164226, 1, 'Salida que va hacia la calle Hollywood, primer piso'),
       ('D1', 14.08564, -87.16401, 1, 'Salida que va hacia la calle Hollywood, primer piso'),
       ('1847', 14.08513, -87.16508, 1, 'Entrada pricipal, frente al Comedor Universitario'),
       ('J1', 14.08616, -87.16656, 1, 'Frente a la entrada a la Clínica Universitaria'),
       ('G1', 14.08376, -87.1657, 1, 'Justo frente a la entrada principal del edificio'),
       ('I1', 14.08413, -87.16757, 1, 'Justo frente a la entrada principal del edificio'),
       ('H1', 14.08386, -87.16656, 1, 'Justo frente a la entrada principal del edificio'),
       ('Alma Máter', 14.08393, -87.16183, 1, 'Frente a la entrada principal del Administrativo'),
       ('A2', 14.0857, -87.16214, 1, 'Justo frente a la entrada principal del edificio'),
       ('B1', 14.08612, -87.16171, 1, 'En la entrada principal del edificio'),
       ('B2', 14.08668, -87.16198, 1, 'En la entrada principal del edificio'),
       ('A1', 14.085004, -87.16212, 1, 'Justo en la entrada principal del edificio'),
       ('CRA-DEGT', 14.0851, -87.16276, 1, 'Frente a la entrada del CRA'),
       ('E1', 14.08388, -87.16238, 1, 'Justo en la entrada principal al edificio'),
       ('CISE', 14.0848, -87.16565, 1, 'Frente al cajero de Banco Atlántida (ATM)'),
       ('Escuela', 14.08683, -87.16762, 1, 'Entrada principal a la Escuela Experimental de la UNAH'),
       ('Polideportivo', 14.08512, -87.17096, 1, 'Entrada principal al Palacio Universitario de los Deportes'),
       ('Observatorio', 14.08742, -87.15945, 1, 'Frente al Observatorio Astronómico Centroamericano'),
       ('Reloj', 14.08492, -87.16496, 1, 'Frente al Reloj Solar');

INSERT INTO [Careers] (Name, Active)
VALUES ('Ingeniería en Sistemas', 1),
       ('Medicina', 1),
       ('Derecho', 1),
       ('Administración de Empresas', 1),
       ('Contaduría Pública', 1),
       ('Periodismo', 1),
       ('Lenguas Extranjeras', 1),
       ('Psicología', 1),
       ('Pedagogía', 1),
       ('Trabajo Social', 1),
       ('Historia', 1),
       ('Letras', 1),
       ('Filosofía', 1),
       ('Sociología', 1),
       ('Educación Física', 1),
       ('Lenguas Extranjeras con Orientación en Inglés y Francés', 1),
       ('Música', 1),
       ('Desarrollo Local', 1),
       ('Ingeniería Civil', 1),
       ('Ingeniería Mecánica Industrial', 1),
       ('Ingeniería Eléctrica Industrial', 1),
       ('Ingeniería Industrial', 1),
       ('Arquitectura', 1),
       ('Comunicación Audiovisual', 1),
       ('Matemática Pura', 1),
       ('Matemáticas Aplicadas', 1),
       ('Física', 1),
       ('Astronomía y Astrofísica', 1),
       ('Odontología', 1),
       ('Nutrición', 1),
       ('Química y Farmacia', 1),
       ('Enfermería', 1),
       ('Microbiología', 1),
       ('Biología', 1),
       ('Fonoaudiología', 1),
       ('Economía', 1),
       ('Contaduría Pública y Finanzas', 1),
       ('Administración Aduanera', 1),
       ('Administración de Banca y Finanzas', 1),
       ('Comercio Internacional', 1),
       ('Informática Administrativa', 1),
       ('Relaciones Internacionales', 1),
       ('Analítica de Datos', 1),
       ('Ingeniería en Protección y Seguridad Privada', 1),
       ('Maquinaria Pesada', 1),
       ('Carreras Agropecuarias', 1),
       ('Biosociología', 1),
       ('Hidrología', 1);


INSERT INTO [Media] (Name, Link)
VALUES ('Media1', 'https://example.com/media1'),
       ('Media2', 'https://example.com/media2'),
       ('Media3', 'https://example.com/media3'),
       ('Media4', 'https://example.com/media4'),
       ('Media5', 'https://example.com/media5'),
       ('Media6', 'https://example.com/media6'),
       ('Media7', 'https://example.com/media7'),
       ('Media8', 'https://example.com/media8'),
       ('Media9', 'https://example.com/media9'),
       ('Media10', 'https://example.com/media10'),
       ('Media11', 'https://example.com/media11'),
       ('Media12', 'https://example.com/media12'),
       ('Media13', 'https://example.com/media13'),
       ('Media14', 'https://example.com/media14'),
       ('Media15', 'https://example.com/media15'),
       ('Media16', 'https://example.com/media16'),
       ('Media17', 'https://example.com/media17'),
       ('Media18', 'https://example.com/media18'),
       ('Media19', 'https://example.com/media19'),
       ('Media20', 'https://example.com/media20');

INSERT INTO [OrderStatus] (Name, Active)
VALUES ('En proceso', 1),
       ('Entregado', 1),
       ('No entregado', 1);

INSERT INTO [UserRoles] (Name, Active)
VALUES ('Poster', 1),
       ('Runner', 1);

INSERT INTO [Users] (Name, DNI, Email, Password, BirthDay, Rating, ProfilePicId, LastLocationId, CareerId)
VALUES ('Juan Perez', '12345678', 'juan.perez@example.com', 'qEYJUU31cXLIQMPkUcYPmNoaiAztAax7Z1U3zLoAu5Y=', '1990-05-15', 4, 1, 1, 5),
       ('Maria Gomez', '87654321', 'maria.gomez@example.com', 'IoWHf77EuxrWRmptNZbzo2lkbbA6AtT3SiYWpQ1Fegw=', '1985-08-22', 5, 2, 2, 10),
       ('Carlos Ruiz', '23456789', 'carlos.ruiz@example.com', 'IoWHf77EuxrWRmptNZbzo2lkbbA6AtT3SiYWpQ1Fegw=', '1992-11-30', 3, 3, 3, 15),
       ('Ana Lopez', '34567890', 'ana.lopez@example.com', 'IoWHf77EuxrWRmptNZbzo2lkbbA6AtT3SiYWpQ1Fegw=', '1988-03-10', 4, 4, 4, 20),
       ('Luis Torres', '45678901', 'luis.torres@example.com', 'IoWHf77EuxrWRmptNZbzo2lkbbA6AtT3SiYWpQ1Fegw=', '1995-07-25', 2, 5, 5, 25),
       ('Sofia Ramirez', '56789012', 'sofia.ramirez@example.com', 'IoWHf77EuxrWRmptNZbzo2lkbbA6AtT3SiYWpQ1Fegw=', '1991-09-12', 5, 6, 6, 30),
       ('Pedro Sanchez', '67890123', 'pedro.sanchez@example.com', 'IoWHf77EuxrWRmptNZbzo2lkbbA6AtT3SiYWpQ1Fegw=', '1987-12-05', 3, 7, 7, 3),
       ('Laura Diaz', '78901234', 'laura.diaz@example.com', 'IoWHf77EuxrWRmptNZbzo2lkbbA6AtT3SiYWpQ1Fegw=', '1993-04-18', 4, 8, 8, 8),
       ('Miguel Castro', '89012345', 'miguel.castro@example.com', 'IoWHf77EuxrWRmptNZbzo2lkbbA6AtT3SiYWpQ1Fegw=', '1989-06-20', 5, 9, 9, 12),
       ('Elena Morales', '90123456', 'elena.morales@example.com', 'IoWHf77EuxrWRmptNZbzo2lkbbA6AtT3SiYWpQ1Fegw=', '1994-02-14', 2, 10, 10, 18);

INSERT INTO [Posts] (Title, Description, SugestedValue, IdPosterUser, IdPickupLocation, IdDeliveryLocation, CreatedAt, Completed, Accepted)
VALUES 
('Pizza Margarita', '¡Hola! ¿Alguien podría traerme una Pizza Margarita desde Pizzería Napoli? Estoy en la zona universitaria y pagaría $15.50', 15.50, 1, 2, 5, '2023-01-10 09:00:00', 0, 0),
('Sushi Variado', 'Busco quien me traiga un combo de Sushi Variado desde Sushi Palace hasta mi oficina en el centro. Ofrezco $30 por el favor', 30.00, 2, 3, 7, '2023-02-15 10:30:00', 0, 0),
('Hamburguesa Doble', '¡Tengo antojo de una Hamburguesa Doble con queso! ¿Alguien viene de Burger Town hacia el parque? Pagaría $12', 12.00, 3, 4, 8, '2023-03-20 08:45:00', 1, 0),
('Ensalada César', 'Necesito una Ensalada César saludable para el almuerzo. Si alguien pasa por Fresh Greens, ofrezco $10', 10.00, 4, 5, 9, '2023-04-25 11:15:00', 0, 0),
('Tacos de Carnitas', '¡Tacos urgentes! ¿Alguien puede traerme Tacos de Carnitas desde Taquería El Michoacano? Estoy en la colonia Reforma', 18.00, 5, 6, 10, '2023-05-30 12:00:00', 1, 0),
('Pasta Alfredo', 'Busco quien me traiga Pasta Alfredo desde Italianissimo para cenar. Ofrezco $22 por el servicio', 22.00, 6, 7, 11, '2023-06-05 07:30:00', 0, 0),
('Helado de Vainilla', '¿Alguien viene de la heladería y me puede traer un Helado de Vainilla? Estoy en casa con antojo dulce :)', 8.00, 7, 8, 12, '2023-07-10 14:20:00', 1, 0),
('Café Americano', 'Necesito un Café Americano grande desde Coffee Time para seguir trabajando. ¡Ayuda por favor!', 5.00, 8, 9, 13, '2023-08-15 16:45:00', 0, 0),
('Tarta de Manzana', '¿Alguien que venga de la pastelería podría traerme una porción de Tarta de Manzana? Es para un cumpleaños sorpresa', 12.00, 9, 10, 14, '2023-09-20 13:10:00', 1, 0),
('Burrito de Pollo', '¡Tengo mucha hambre! ¿Alguien puede traerme un Burrito de Pollo desde Chipotle? Estoy en el edificio de oficinas 3', 14.00, 10, 11, 15, '2023-10-25 09:55:00', 0, 0),
('Lasagna de Carne', 'Busco quien me traiga Lasagna de Carne desde Trattoria Bella para la cena familiar de hoy', 20.00, 1, 12, 16, '2023-11-30 10:40:00', 1, 0),
('Sopa de Tomate', 'No me siento bien, ¿alguien podría traerme una Sopa de Tomate calentita desde Soup Factory?', 9.00, 2, 13, 17, '2023-12-05 08:25:00', 0, 0),
('Milanesa con Papas', '¡Antojo de comida casera! ¿Quién me puede traer Milanesa con Papas desde El Rincón Familiar?', 18.00, 3, 14, 18, '2024-01-10 11:50:00', 1, 0),
('Empanadas de Jamón y Queso', 'Necesito 6 Empanadas de Jamón y Queso para una reunión rápida. ¿Alguien puede ayudar?', 15.00, 4, 15, 1, '2024-02-15 12:35:00', 0, 0),
('Churros con Chocolate', 'Buscando Churros con Chocolate recién hechos para compartir con mi novia. ¡Gracias!', 10.00, 5, 16, 2, '2024-03-20 09:20:00', 1, 0),
('Ceviche de Pescado', '¿Alguien viene de la costa y puede traerme Ceviche de Pescado fresco? Es para una ocasión especial', 25.00, 6, 17, 3, '2024-04-25 15:05:00', 0, 0),
('Ramen de Cerdo', 'Día frío, necesito un Ramen de Cerdo auténtico. ¿Alguien pasa por Ramen Ya hoy?', 28.00, 7, 18, 4, '2024-05-30 10:10:00', 1, 0),
('Brownie con Helado', 'Antojo dulce: ¿quién me puede traer Brownie con Helado desde The Cheesecake Shop?', 12.00, 8, 1, 5, '2024-06-05 07:55:00', 0, 0),
('Sandwich de Pavo', 'Busco un Sandwich de Pavo saludable para el almuerzo de hoy. ¡Gracias!', 11.00, 9, 2, 6, '2024-07-10 13:40:00', 1, 0),
('Batido de Fresa', '¿Alguien puede traerme un Batido de Fresa grande desde Juice Bar? Estoy en el gimnasio', 7.00, 10, 3, 7, '2024-08-15 16:15:00', 0, 0);

INSERT INTO [Offers] (CounterOfferAmount, IdPost, IdUserCreator, CreatedAt, IsCounterOffer, Accepted)
VALUES (15.50, 1, 2, '2023-01-12 10:15:30', 0, 1),   -- Igual al SuggestedValue del Post 1
       (30.50, 2, 3, '2023-02-16 14:20:45', 1, 0),   -- Distinto al SuggestedValue del Post 2
       (12.00, 3, 4, '2023-03-21 09:05:10', 0, 1),   -- Igual al SuggestedValue del Post 3
       (11.00, 4, 5, '2023-04-26 16:30:00', 1, 0),   -- Distinto al SuggestedValue del Post 4
       (18.00, 5, 6, '2023-05-31 11:45:15', 0, 1),   -- Igual al SuggestedValue del Post 5
       (22.50, 6, 7, '2023-06-06 08:10:20', 1, 0),   -- Distinto al SuggestedValue del Post 6
       (8.00, 7, 8, '2023-07-11 13:25:35', 0, 1),    -- Igual al SuggestedValue del Post 7
       (5.50, 8, 9, '2023-08-16 17:40:50', 1, 0),    -- Distinto al SuggestedValue del Post 8
       (12.00, 9, 10, '2023-09-21 12:55:05', 0, 1),  -- Igual al SuggestedValue del Post 9
       (14.50, 10, 1, '2023-10-26 18:00:10', 1, 0),  -- Distinto al SuggestedValue del Post 10
       (20.00, 11, 2, '2023-12-01 07:15:25', 0, 1),  -- Igual al SuggestedValue del Post 11
       (9.50, 12, 3, '2023-12-06 19:20:30', 1, 0),   -- Distinto al SuggestedValue del Post 12
       (18.00, 13, 4, '2024-01-11 10:35:45', 0, 1),  -- Igual al SuggestedValue del Post 13
       (15.50, 14, 5, '2024-02-16 14:50:00', 1, 0),  -- Distinto al SuggestedValue del Post 14
       (10.00, 15, 6, '2024-03-21 09:05:15', 0, 1),  -- Igual al SuggestedValue del Post 15
       (25.50, 16, 7, '2024-04-26 16:20:30', 1, 0),  -- Distinto al SuggestedValue del Post 16
       (28.00, 17, 8, '2024-05-31 11:35:45', 0, 1),  -- Igual al SuggestedValue del Post 17
       (12.50, 18, 9, '2024-06-06 08:50:00', 1, 0),  -- Distinto al SuggestedValue del Post 18
       (11.00, 19, 10, '2024-07-11 13:05:15', 0, 1), -- Igual al SuggestedValue del Post 19
       (7.50, 20, 1, '2024-08-16 17:20:30', 1, 0); -- Distinto al SuggestedValue del Post 20

INSERT INTO [Mandaditos] (SecurityCode, AcceptedAt, DeliveredAt, IdPost, IdOffer, AcceptedRate)
VALUES ('ABC123', '2023-01-12 10:30:00', '2023-01-12 12:00:00', 1, 1, 15.50), -- IdPost 1, IdOffer 1
       ('DEF456', '2023-02-16 14:45:00', '2023-02-16 16:00:00', 2, 2, 30.50), -- IdPost 2, IdOffer 2
       ('GHI789', '2023-03-21 09:15:00', '2023-03-21 10:30:00', 3, 3, 12.00), -- IdPost 3, IdOffer 3
       ('JKL012', '2023-04-26 16:30:00', '2023-04-26 18:00:00', 4, 4, 11.00), -- IdPost 4, IdOffer 4
       ('MNO345', '2023-05-31 11:45:00', '2023-05-31 13:00:00', 5, 5, 18.00), -- IdPost 5, IdOffer 5
       ('PQR678', '2023-06-06 08:20:00', '2023-06-06 09:30:00', 6, 6, 22.50), -- IdPost 6, IdOffer 6
       ('STU901', '2023-07-11 13:25:00', '2023-07-11 14:45:00', 7, 7, 8.00),  -- IdPost 7, IdOffer 7
       ('VWX234', '2023-08-16 17:40:00', '2023-08-16 19:00:00', 8, 8, 5.50),  -- IdPost 8, IdOffer 8
       ('YZA567', '2023-09-21 12:55:00', null, 9, 9, 12.00); -- IdPost 9, IdOffer 9

INSERT INTO [OrderStatusHistories] (IdStatus, ChangeAt, IdMandadito, Active)
VALUES (1, '2023-01-12 10:30:00', 1, 1),
       (2, '2023-01-12 12:00:00', 1, 1),
       (1, '2023-02-16 14:45:00', 2, 1),
       (2, '2023-02-16 16:00:00', 2, 1),
       (1, '2023-03-21 09:15:00', 3, 1),
       (2, '2023-03-21 10:30:00', 3, 1),
       (2, '2023-09-21 12:55:00', 9, 1),
       (3, '2023-09-21 13:20:00', 9, 1);

INSERT INTO Ratings (IdMandadito, IdRater, IdRatedUser, RatingNum, Review, IdRatedRole, CreatedAt)
VALUES (1, 2, 1, 5, '¡Excelente servicio! Todo llegó a tiempo y en perfecto estado.', 1,
        '2023-01-12 12:30:00'), -- IdMandadito 1, IdRatedRole 1 (cliente)
       (1, 1, 2, 4, 'Muy amable y puntual. ¡Lo recomiendo!', 2,
        '2023-01-12 12:35:00'), -- IdMandadito 1, IdRatedRole 2 (repartidor)
       (2, 3, 2, 3, 'Buen servicio, pero hubo un pequeño retraso.', 1,
        '2023-02-16 16:15:00'), -- IdMandadito 2, IdRatedRole 1 (cliente)
       (2, 2, 3, 5, 'Todo perfecto, muy profesional.', 2,
        '2023-02-16 16:20:00'), -- IdMandadito 2, IdRatedRole 2 (repartidor)
       (3, 4, 3, 4, 'Muy buen trato, pero el paquete llegó un poco golpeado.', 1,
        '2023-03-21 10:45:00'), -- IdMandadito 3, IdRatedRole 1 (cliente)
       (3, 3, 4, 5, 'Excelente comunicación y servicio.', 2,
        '2023-03-21 10:50:00'), -- IdMandadito 3, IdRatedRole 2 (repartidor)
       (4, 5, 4, 2, 'El servicio fue lento y no cumplió con lo esperado.', 1,
        '2023-04-26 18:30:00'), -- IdMandadito 4, IdRatedRole 1 (cliente)
       (4, 4, 5, 3, 'Cumplió, pero podría mejorar en puntualidad.', 2,
        '2023-04-26 18:35:00'), -- IdMandadito 4, IdRatedRole 2 (repartidor)
       (5, 6, 5, 5, '¡Increíble! Todo perfecto y muy rápido.', 1,
        '2023-05-31 13:15:00'), -- IdMandadito 5, IdRatedRole 1 (cliente)
       (5, 5, 6, 4, 'Muy buen servicio, pero el trato podría ser más amable.', 2,
        '2023-05-31 13:20:00'); -- IdMandadito 5, IdRatedRole 2 (repartidor)

INSERT INTO ManagementRole (Name, Active)
VALUES ('Cambio de contraseña', 1)
