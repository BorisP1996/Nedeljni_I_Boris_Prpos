# Nedeljni_I_Boris_Prpos
Generisanje random stringa u fajlu koji ce se koristiti za kreiranje menadzera
3 pokusaja za unos pravilne sifre prilikom kreiranja menadzera, samo jednom se moze kreirati nakon pokretanja aplikacije
Validacija JMBG-a
Validacija unique username-a
Kreiranje Employe-a (samo ukoliko postoji manager)
Employe dobija random menagera


Prilikom kreiranja menadzera dodaje se "WPF" na kraju rezervne sifre, a mail mora biti unique kao i username i jmbg

Sistemski admin: 
	vidi sve sektore
	kreira poziciju i sektor
	moguce je kreirati 15 sektora(ne racunajuci default sektor)
	ime sektora mora biti unique
	ime pozicije mora biti unique
	nakon brisanja sektora svi zaposleni u tom sektoru dobijaju default sektor

Lokalni admin:
	vidi sve menadzere i radnike (employe)
	dodjeljuje nivo zaduzenja menadzeru
	brise i edituje employe-a
	ukoliko nema sektora employe se ne moze obrisati niti azurirati

Timski admin:
	vidi sve menadzere i radnike 
	brise i edituje sve menadzere i radnike
	prilikom brisanja menadzera brisu se i svi radnici podredjeni tom menadzeru,ispisuje se broj obrisanih radnika

WPFMaster nalog:
	Vidi sve admine
	Kreira admina, datum isteka naloga se postavlja za 7 dana
