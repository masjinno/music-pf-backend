#!/bin/bash

EXAMPLE="example: "
EXAMPLE_COMMENTED="# example: "

# music-pf-api*.yaml �̏����𖞂����t�@�C���ɑ΂��āA
# AWS��APIGateway�ɃC���|�[�g�\��swagger�`���ɕϊ�����B
# ���s��Aaws-apigateway_�̃v���t�B�b�N�X���t����yaml�t�@�C����
# APIGateway�ɃC���|�[�g�����OK�B

for file in `\find -name 'music-pf-api*.yaml'`;
do
  echo "${file}:"
  OUTPUT_FILE="./aws-apigateway_${file:2:999}"
  sed -e "s/${EXAMPLE}/${EXAMPLE_COMMENTED}/g" ${file} \
    > "${OUTPUT_FILE}"
done
