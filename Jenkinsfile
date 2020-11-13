pipeline {
    agent any

    environment {
        BUILD_NUM_ENV = currentBuild.getNumber()
    }

    stages {
        stage('Temizlik') { 
            steps {
                sh 'dotnet clean' 
            }
        }

        stage('Paket Yükleme') { 
            steps {
                sh 'dotnet restore' 
            }
        }

        stage('Derleme') { 
            steps {
                sh 'dotnet build --configuration Release --no-restore' 
            }
        }

        stage('Statik Kod Analizi') { 
            steps {
                sh 'dotnet clean'
                sh 'dotnet tool list -g'

                withSonarQubeEnv('SonarQube') {
                    sh '$HOME/.dotnet/tools/dotnet-sonarscanner begin /k:DotNetOrnek /n:"Örnek DotNet Core Uygulaması" /v:"$BUILD_NUM_ENV"'
                    sh 'dotnet build --no-restore'
                    sh '$HOME/.dotnet/tools/dotnet-sonarscanner end'
                }
            }
        }

        stage("Kalite Kapısı") {
            steps {
                timeout(time: 1, unit: 'HOURS') {
                    waitForQualityGate abortPipeline: true
                }
            }
        }

        stage('Test') { 
            steps {
                sh 'dotnet test --logger:"trx;LogFileName=unit_tests.testresults"' 
            }
            post {
                always {
                    xunit([MSTest(deleteOutputFiles: true, failIfNotNew: true, pattern: '**/*.testresults', skipNoTestFiles: false, stopProcessingIfError: true)])
                }
            }
        }
    }
}
