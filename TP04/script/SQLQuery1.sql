-- click droit sur la bd pour exécuter dans visual studio
-- si erreur actualiser le serveur, ouvrir le dossier proc stockée et supprimer et relancer
 USE BdH2026


DROP TABLE IF EXISTS dbo.ComProd
DROP TABLE IF EXISTS dbo.Commande
DROP TABLE IF EXISTS dbo.ProduitCaracteristique
DROP TABLE IF EXISTS dbo.Produit
DROP TABLE IF EXISTS dbo.Categorie
DROP TABLE IF EXISTS dbo.Caracteristique
DROP TABLE IF EXISTS dbo.Vendeur
DROP TABLE IF EXISTS dbo.Client
DROP TABLE IF EXISTS dbo.Pays
DROP TABLE IF EXISTS dbo.Province

CREATE TABLE Province
(
	Code_Province VARCHAR(2)  PRIMARY KEY  Not Null, 
	Nom VARCHAR(15)

);
CREATE TABLE Pays
(
	Code_Pays VARCHAR(3)  PRIMARY KEY  Not Null, 
	Nom VARCHAR(15)

);


INSERT INTO PAYS VALUES ('CAD','Canada')
INSERT INTO PAYS VALUES ('USA','Etats-Unis')
INSERT INTO PROVINCE VALUES ('QC','Québec')
INSERT INTO PROVINCE VALUES ('ON','Ontario')


CREATE TABLE Client ( 
Id_client INT IDENTITY(1,1) PRIMARY KEY  Not Null,  
Nom VARCHAR(55) , 
Telephone VARCHAR(10) , 
Adresse_ligne1  VARCHAR(50) , 
Adresse_ligne2  VARCHAR(50), 
Appartement   VARCHAR(10), 
Ville VARCHAR(100) , 
CodePostal  VARCHAR(10) , 
Code_Province VARCHAR(2) , 
Code_Pays VARCHAR(3) , 
Remarque  VARCHAR(20), 
Solde DECIMAL(15,2) , 
HistoTotalCom DECIMAL(15,2) ,  
HistoTotalExpedie DECIMAL(15,2) , 
HistoTotalFacture DECIMAL(15,2) , 
HistoTotalPaiement DECIMAL(15,2) ,
FOREIGN KEY (Code_Province) REFERENCES dbo.Province(Code_Province),
	FOREIGN KEY (Code_Pays) REFERENCES dbo.Pays(Code_Pays) 
);
INSERT INTO Client(Nom,Telephone,Adresse_ligne1,Ville,CodePostal,Code_Province,Code_Pays,Solde,HistoTotalCom,HistoTotalExpedie,HistoTotalFacture,HistoTotalPaiement) 
VALUES ('Rose Lafleur','8191234567','2 rue Fleury','Shawinigan','G9X 1Y7','QC','CAD',0,72.0,0,0,0)
INSERT INTO Client(Nom,Telephone,Adresse_ligne1,Ville,CodePostal,Code_Province,Code_Pays,Solde,HistoTotalCom,HistoTotalExpedie,HistoTotalFacture,HistoTotalPaiement) 
VALUES ('Pierre LaRoche','8191212123','5 rue DuRocher','Grand-Mère','G8VX 2B9','QC','CAD',0,0,0,0,0)


GO