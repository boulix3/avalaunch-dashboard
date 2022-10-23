#!/bin/bash
for file in *.json
do
  Nethereum.Generator.Console generate from-abi -abi $file -o ../Avalaunch -ns Avalaunch
done
