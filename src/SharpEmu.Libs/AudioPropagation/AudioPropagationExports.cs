// Copyright (C) 2026 SharpEmu Emulator Project
// SPDX-License-Identifier: GPL-2.0-or-later

using System.Buffers.Binary;
using SharpEmu.HLE;

namespace SharpEmu.Libs.AudioPropagation;

/// <summary>
/// Minimal libSceAudioPropagation surface. Titles built on this library
/// (observed: Team Asobi engine) treat a failing
/// sceAudioPropagationSystemQueryMemory as fatal and abort via a guest trap,
/// so the whole API succeeds as a no-op: propagation queries report zero
/// paths/rays, which callers handle as "no environmental audio" rather than
/// an error.
/// </summary>
public static class AudioPropagationExports
{
    // Generous no-op working-memory request: callers allocate this block and
    // hand it back to SystemCreate, which ignores it.
    private const ulong SystemMemorySize = 4UL * 1024 * 1024;
    private const ulong SystemMemoryAlignment = 0x100;

    [SysAbiExport(
        Nid = "7xyAxrusLko",
        ExportName = "sceAudioPropagationSystemQueryMemory",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SystemQueryMemory(CpuContext ctx)
    {
        var memoryInfoAddress = ctx[CpuRegister.Rsi];
        if (memoryInfoAddress == 0)
        {
            return ctx.SetReturn((int)OrbisGen2Result.ORBIS_GEN2_ERROR_INVALID_ARGUMENT);
        }

        Span<byte> memoryInfo = stackalloc byte[0x10];
        BinaryPrimitives.WriteUInt64LittleEndian(memoryInfo[0x00..], SystemMemorySize);
        BinaryPrimitives.WriteUInt64LittleEndian(memoryInfo[0x08..], SystemMemoryAlignment);

        return ctx.Memory.TryWrite(memoryInfoAddress, memoryInfo)
            ? ctx.SetReturn(0)
            : ctx.SetReturn((int)OrbisGen2Result.ORBIS_GEN2_ERROR_MEMORY_FAULT);
    }

    [SysAbiExport(
        Nid = "aNEqtSHdUSo",
        ExportName = "sceAudioPropagationSystemCreate",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SystemCreate(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "x5VPqg5iyAk",
        ExportName = "sceAudioPropagationSystemDestroy",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SystemDestroy(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "B2KI2AachWE",
        ExportName = "sceAudioPropagationSystemLock",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SystemLock(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "GrA9ke1QT+E",
        ExportName = "sceAudioPropagationSystemQueryInfo",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SystemQueryInfo(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "kIdb+iQUzCs",
        ExportName = "sceAudioPropagationSystemSetAttributes",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SystemSetAttributes(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "VlBT16890mA",
        ExportName = "sceAudioPropagationSystemSetRays",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SystemSetRays(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "ht-QXT3zGxo",
        ExportName = "sceAudioPropagationSystemGetRays",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SystemGetRays(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "CPLV6G-eXmk",
        ExportName = "sceAudioPropagationSystemRegisterMaterial",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SystemRegisterMaterial(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "XKCN4gpeYsM",
        ExportName = "sceAudioPropagationSystemUnregisterMaterial",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SystemUnregisterMaterial(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "8bI5h8req30",
        ExportName = "sceAudioPropagationRoomCreate",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int RoomCreate(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "S0JwP2AFTTE",
        ExportName = "sceAudioPropagationRoomDestroy",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int RoomDestroy(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "b-dYXrjSNZU",
        ExportName = "sceAudioPropagationPortalCreate",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int PortalCreate(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "ZQXE-xS6MTE",
        ExportName = "sceAudioPropagationPortalDestroy",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int PortalDestroy(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "WXMhENV2NcA",
        ExportName = "sceAudioPropagationPortalSetAttributes",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int PortalSetAttributes(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "d84otraxt2s",
        ExportName = "sceAudioPropagationSourceCreate",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SourceCreate(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "wkseM3LWPuc",
        ExportName = "sceAudioPropagationSourceDestroy",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SourceDestroy(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "-wsUTr31yeg",
        ExportName = "sceAudioPropagationSourceSetAttributes",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SourceSetAttributes(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "PBcrVpEqUVY",
        ExportName = "sceAudioPropagationSourceCalculateAudioPaths",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SourceCalculateAudioPaths(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "G+QLTfyLMYk",
        ExportName = "sceAudioPropagationSourceGetAudioPathCount",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SourceGetAudioPathCount(CpuContext ctx)
    {
        // (source, uint32_t* outCount) shape assumed from the sibling
        // GetAudioPath accessors: report zero propagation paths so callers
        // skip environmental processing entirely.
        var countAddress = ctx[CpuRegister.Rsi];
        if (countAddress != 0 && !ctx.TryWriteUInt32(countAddress, 0))
        {
            return ctx.SetReturn((int)OrbisGen2Result.ORBIS_GEN2_ERROR_MEMORY_FAULT);
        }

        return ctx.SetReturn(0);
    }

    [SysAbiExport(
        Nid = "eEeKqFeNI3o",
        ExportName = "sceAudioPropagationSourceGetAudioPath",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SourceGetAudioPath(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "tKSmk2JsMAA",
        ExportName = "sceAudioPropagationSourceSetAudioPath",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SourceSetAudioPath(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "5vzOS2pHMFc",
        ExportName = "sceAudioPropagationSourceSetAudioPaths",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SourceSetAudioPaths(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "aKJZx7wCma8",
        ExportName = "sceAudioPropagationSourceGetRays",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SourceGetRays(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "3aEY9tPXGKc",
        ExportName = "sceAudioPropagationSourceQueryInfo",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SourceQueryInfo(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "hhz9pITnC8k",
        ExportName = "sceAudioPropagationSourceRender",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int SourceRender(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "tL2AEPejVQE",
        ExportName = "sceAudioPropagationPathGetNumPoints",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int PathGetNumPoints(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "BbOT4vBwAjs",
        ExportName = "sceAudioPropagationResetAttributes",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int ResetAttributes(CpuContext ctx) => ctx.SetReturn(0);

    [SysAbiExport(
        Nid = "gCmQm6dvMxw",
        ExportName = "sceAudioPropagationReportApi",
        Target = Generation.Gen5,
        LibraryName = "libSceAudioPropagation")]
    public static int ReportApi(CpuContext ctx) => ctx.SetReturn(0);
}
