import { Address } from "./address"
import { AnalysisResultFile } from "./analysisResultFile"
import { MedicalExpertise } from "./medicalExpertise"
import { Photo } from "./photo"

export interface Member {
    id: number
    userName: string
    name: string
    lastName: string
    photo: string //photo url
    analysisResultFile: any
    age: number
    created: string
    lastActive: string
    gender: string
    description: string
    languageSpoken: string
    photos: Photo[]
    addresses: Address[]
    analysisResultFiles: AnalysisResultFile[]
    medicalExpertises: MedicalExpertise[]
  }
  
