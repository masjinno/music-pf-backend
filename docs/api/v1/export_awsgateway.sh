#!/bin/bash

EXAMPLE="example: "
EXAMPLE_COMMENTED="# example: "

# music-pf-api*.yaml の条件を満たすファイルに対して、
# AWSのAPIGatewayにインポート可能なswagger形式に変換する。
# 実行後、aws-apigateway_のプレフィックスが付いたyamlファイルを
# APIGatewayにインポートすればOK。

for file in `\find -name 'music-pf-api*.yaml'`;
do
  echo "${file}:"
  OUTPUT_FILE="./aws-apigateway_${file:2:999}"
  sed -e "s/${EXAMPLE}/${EXAMPLE_COMMENTED}/g" ${file} \
    > "${OUTPUT_FILE}"
done
