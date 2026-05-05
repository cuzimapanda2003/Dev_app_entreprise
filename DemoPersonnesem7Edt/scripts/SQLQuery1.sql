


USE bdDemoPersonne
GO
 DROP TABLE IF EXISTS dbo.PersonneChaussure ;
 DROP TABLE IF EXISTS dbo.Personne;   
 DROP TABLE IF EXISTS dbo.Province;
 DROP TABLE IF EXISTS dbo.Profession;

 DROP TABLE IF EXISTS dbo.Chaussure;
 DROP TABLE IF EXISTS dbo.GenreChaussure;
 DROP TABLE IF EXISTS dbo.TypeChaussure;
 Go
CREATE TABLE TypeChaussure (
  Code_typeChaussure VARCHAR(4) PRIMARY KEY,
  Description VARCHAR(15),
  
  );
  INSERT INTO TypeChaussure( Code_typeChaussure,Description)  VALUES ('Sand','sandales')
  INSERT INTO TypeChaussure( Code_typeChaussure,Description)  VALUES ('BotH','bottes hiver')
  INSERT INTO TypeChaussure( Code_typeChaussure,Description)  VALUES ('Soul','souliers')
  INSERT INTO TypeChaussure( Code_typeChaussure,Description)  VALUES ('BotE','bottes eau')
Go
CREATE TABLE GenreChaussure (
  Code_genreChaussure VARCHAR(2) PRIMARY KEY,
  Description VARCHAR(15),
  
  );
  INSERT INTO GenreChaussure( Code_genreChaussure,Description)  VALUES ('F ','Femmes')
  INSERT INTO GenreChaussure( Code_genreChaussure,Description)  VALUES ('H ','Homme')
  INSERT INTO GenreChaussure( Code_genreChaussure,Description)  VALUES ('E ','Enfants')
Go
CREATE TABLE Chaussure (
  Id_chaussure  INT identity(1, 1) NOT NULL PRIMARY KEY,
  Taille decimal(5, 2),
  Code_genreChaussure VARCHAR(2),
  Code_typeChaussure VARCHAR(4),
  Prix decimal(12,2),
  FOREIGN KEY(Code_genreChaussure) REFERENCES dbo.Genrechaussure(Code_genreChaussure),
  FOREIGN KEY (Code_typeChaussure) REFERENCES dbo.TypeChaussure(Code_typeChaussure)
  
  );
  GO
   INSERT INTO Chaussure  VALUES (7.0,'F ','Sand',10.0)
   INSERT INTO Chaussure  VALUES (7.5,'F ','Sand',10.0)
   INSERT INTO Chaussure  VALUES (10.5,'F ','Sand',10.0)
Go
 CREATE TABLE Province (
  Code_Province VARCHAR(2) PRIMARY KEY,
  Nom VARCHAR(15),
  
  );
  INSERT INTO Province( Code_Province,Nom)  VALUES ('QC','Québec')
  INSERT INTO Province( Code_Province,Nom)  VALUES ('ON','Ontario')
  Go


 CREATE TABLE Profession (
  Id_Profession INT  PRIMARY KEY,
  Nom VARCHAR(15),
  
  );
  INSERT INTO Profession( Id_Profession,Nom)  VALUES (1,'Étudiant')
  INSERT INTO Profession( Id_Profession,Nom)  VALUES (2,'programmeur')
	  GO	

CREATE TABLE dbo.Personne 
(
    Id_Personne INT identity(1, 1) NOT NULL PRIMARY KEY,
    Prenom varchar(15) ,
    NomFamille varchar(15) ,
    Age INT ,
    Actif TiNyINT,
    Date_CreationBidon datetime,
    Total_Ventes decimal(12, 2),
    Id_Profession INT NOT NULL,
    Code_Province varchar(2) NOT NULL,
    FOREIGN KEY(Id_Profession) REFERENCES dbo.Profession(Id_Profession),
    FOREIGN KEY (Code_Province) REFERENCES dbo.Province(Code_Province)

);
GO
  INSERT INTO Personne(NomFamille,Prenom,Age,Date_CreationBidon,aCTIF, Code_Province ,Id_Profession,tOTAL_VENTES)  VALUES ('NomFam1','Prénom1',11,'01-sep-2020',0,'QC',1,0)
  INSERT INTO Personne VALUES ('NomFam2','Prénom2',12,1,GetDate(),15.15,2,'QC')
  INSERT INTO Personne VALUES ('NomFam3','Prénom3',13,0,GetDate()-2,100,2,'ON')
  GO

  CREATE TABLE dbo.PersonneChaussure 
(
   Id_personneChaussure INT identity(1, 1) NOT NULL PRIMARY KEY,
   Id_personne INT NOT NULL,
   Id_chaussure INT NOT NULL,
    Prix_vente Decimal(12,2),
    Qte_stock INT ,
 
  );
  
GO
   INSERT INTO PersonneChaussure  VALUES (1,1,12.0,3)
 
    
