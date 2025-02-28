using System;
using VehicleApp.Domain.Shared.Enums;
using Volo.Abp.Application.Dtos;

namespace VehicleApp.Application.Contracts.Vehicle.Dtos;

/// <summary>
/// �������ݴ�����󣬼̳��� AuditedEntityDto<Guid>�������ڲ�ͬ��֮�䴫�䳵����������Ϣ��
/// ͨ������չʾ��������ϸ��Ϣ�������˳����Ļ������ԡ�״̬�Լ��������ŵ����������Ϣ��
/// </summary>
public class VehicleDto : AuditedEntityDto<Guid>
{
    /// <summary>
    /// ����ʶ����루VIN��������Ψһ��ʶ������
    /// </summary>
    public string VIN { get; set; }

    /// <summary>
    /// ���������̣���������ȡ�
    /// </summary>
    public string Make { get; set; }

    /// <summary>
    /// �����ͺţ��翨������X5 �ȡ�
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// ����������ݡ�
    /// </summary>
    public string Year { get; set; }

    /// <summary>
    /// ������ɫ��
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    /// ������ʻ�������
    /// </summary>
    public int Mileage { get; set; }

    /// <summary>
    /// �������պ��롣
    /// </summary>
    public string LicensePlate { get; set; }

    /// <summary>
    /// �������������ͣ�����ͷ����������ͷ������ȡ�
    /// </summary>
    public EngineType EngineType { get; set; }

    /// <summary>
    /// �������������ͣ����Զ������䡢�ֶ�������ȡ�
    /// </summary>
    public TransmissionType TransmissionType { get; set; }

    /// <summary>
    /// ����ȼ�����ͣ�����͡����͡���Ȼ���ȡ�
    /// </summary>
    public FuelType FuelType { get; set; }

    /// <summary>
    /// ����״̬������á�ά���С��ѳ���ȡ�
    /// </summary>
    public VehicleStatus Status { get; set; }

    /// <summary>
    /// ���������ŵ��Ψһ��ʶ��
    /// </summary>
    public Guid StoreId { get; set; }

    /// <summary>
    /// ���������ߵ�Ψһ��ʶ�������߿������û���˾��
    /// </summary>
    public Guid OwnerId { get; set; }
}
