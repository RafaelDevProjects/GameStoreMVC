-- ============================================
--   GameStoreMVC - Script de Criação do Banco
--   Disciplina: C# Development - FIAP 3ESPR
-- ============================================

-- 1. Criar e selecionar o banco
CREATE DATABASE IF NOT EXISTS GameStoreDB
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE GameStoreDB;

-- ============================================
-- 2. Tabela de Usuários
-- ============================================
CREATE TABLE IF NOT EXISTS Usuarios (
    Id          INT             NOT NULL AUTO_INCREMENT,
    Nome        VARCHAR(100)    NOT NULL,
    Email       VARCHAR(200)    NOT NULL,
    SenhaHash   LONGTEXT        NOT NULL,
    IsAdmin     TINYINT(1)      NOT NULL DEFAULT 0,
    CriadoEm   DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (Id),
    UNIQUE INDEX IX_Usuarios_Email (Email)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ============================================
-- 3. Tabela de Games
-- ============================================
CREATE TABLE IF NOT EXISTS Games (
    Id          INT             NOT NULL AUTO_INCREMENT,
    Titulo      VARCHAR(200)    NOT NULL,
    Descricao   VARCHAR(1000)   NOT NULL,
    Preco       DECIMAL(10,2)   NOT NULL,
    UrlCapa     VARCHAR(500)    NULL,
    Categoria   VARCHAR(50)     NOT NULL DEFAULT 'Ação',
    Destaque    TINYINT(1)      NOT NULL DEFAULT 0,
    CriadoEm   DATETIME        NOT NULL DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ============================================
-- 4. Seed: Usuário Administrador
--    Senha: Admin@123  (hash BCrypt)
-- ============================================
INSERT INTO Usuarios (Nome, Email, SenhaHash, IsAdmin, CriadoEm)
VALUES (
    'Administrador',
    'admin@gamestore.com',
    '$2a$11$9GzSNGxDQXcf6F6W4vE5g.YT8eHK3kqPzDU1bVbFmITFcgYkp.6Iq',
    1,
    NOW()
);

-- ============================================
-- 5. Seed: Jogos de exemplo
-- ============================================
INSERT INTO Games (Titulo, Descricao, Preco, UrlCapa, Categoria, Destaque, CriadoEm) VALUES
(
    'Elden Ring',
    'Um RPG de ação épico em um vasto mundo aberto repleto de desafios e mistérios.',
    199.90,
    'https://images.igdb.com/igdb/image/upload/t_cover_big/co4jni.jpg',
    'RPG',
    1,
    NOW()
),
(
    'God of War Ragnarök',
    'Kratos e Atreus embarcam em uma jornada épica pelos Nove Reinos da mitologia nórdica.',
    249.90,
    'https://images.igdb.com/igdb/image/upload/t_cover_big/co5s5v.jpg',
    'Ação',
    1,
    NOW()
),
(
    'Forza Horizon 5',
    'O maior e mais diversificado mundo de Forza Horizon já criado, ambientado no México.',
    149.90,
    'https://images.igdb.com/igdb/image/upload/t_cover_big/co3ofx.jpg',
    'Corrida',
    0,
    NOW()
),
(
    'The Legend of Zelda: Tears of the Kingdom',
    'Link explora os céus e as terras de Hyrule em uma aventura de mundo aberto inesquecível.',
    299.90,
    'https://images.igdb.com/igdb/image/upload/t_cover_big/co6cl7.jpg',
    'Aventura',
    1,
    NOW()
),
(
    'Hogwarts Legacy',
    'Viva a experiência de ser um estudante de magia no mundo mágico do século XIX.',
    179.90,
    'https://images.igdb.com/igdb/image/upload/t_cover_big/co4xl3.jpg',
    'RPG',
    0,
    NOW()
),
(
    'Street Fighter 6',
    'A lenda retorna com novos lutadores, novos modos e uma jogabilidade revolucionária.',
    199.90,
    'https://images.igdb.com/igdb/image/upload/t_cover_big/co69vj.jpg',
    'Ação',
    0,
    NOW()
);

-- ============================================
-- 6. Verificar resultado
-- ============================================
SELECT 'Banco criado com sucesso!' AS Status;
SELECT CONCAT('Usuarios: ', COUNT(*), ' registros') AS Info FROM Usuarios
UNION ALL
SELECT CONCAT('Games: ', COUNT(*), ' registros') FROM Games;
