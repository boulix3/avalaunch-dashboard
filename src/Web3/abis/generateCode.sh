#!/bin/bash
for file in Sale*.json
do
  Nethereum.Generator.Console generate from-abi -abi $file -o ../Avalaunch -ns Avalaunch
done
