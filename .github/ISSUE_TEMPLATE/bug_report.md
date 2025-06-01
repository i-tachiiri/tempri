name: バグ報告
description: 不具合を報告します
title: "[Bug] "
labels: ["bug"]

body:
  - type: textarea
    id: summary
    attributes:
      label: 不具合の内容
      description: どんな問題が起きているか、簡潔に書いてください
    validations:
      required: true
