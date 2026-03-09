create database dbApp;
use dbApp;

create table tbUser(
	userId int primary key auto_increment,
	userName varchar(100) not null,
	email varchar(100) unique not null,
	userPassword varchar(150) not null check(char_length(userPassword) > 8),
	cpf varchar(14) not null,
	birthDate date not null
);

create procedure spCreateUser(
	vUserName varchar(100),
	vEmail varchar(100),
	vUserP varchar(100),
	vCpf varchar(14),
	vBirthDate date
)
begin
	if not exists(select * from tbUser where email = vEmail limit 1)
	then
		insert into tbUser(userName, email, userPassword, cpf, birthDate)
			values(vUserName, vEmail, vUserP, vCpf, vBirthDate);
	end if;
end;