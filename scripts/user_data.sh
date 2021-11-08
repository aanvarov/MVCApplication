#!/bin/bash
sudo yum -y update
sudo yum -y install ruby
sudo yum -y install wget
cd /home/ec2-user
wget https://aws-codedeploy-eu-west-2.s3.eu-west-2.amazonaws.com/latest/install
chmod +x ./install
sudo ./install auto
sudo rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm
sudo yum install dotnet-sdk-5.0
sudo yum install aspnetcore-runtime-5.0
sudo yum install dotnet-runtime-5.0
